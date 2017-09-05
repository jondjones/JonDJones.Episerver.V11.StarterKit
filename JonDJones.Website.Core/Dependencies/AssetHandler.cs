namespace JonDJones.Website.Core.Dependencies
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.DataAbstraction;
    using EPiServer.DataAccess;
    using EPiServer.Framework.Blobs;
    using EPiServer.Security;
    using Interfaces;
    using log4net;

    using JonDJones.Website.Core.EpiserverConfiguration.Media;
    using JonDJones.Website.Shared.Helpers;
    using JonDJones.Website.Shared.Resources;

    public class AssetHandler : IAssetHandler
    {
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IContentRepository _contentRepository;

        private readonly IBlobFactory _blobFactory;

        private readonly IContextResolver _contextResolver;

        private readonly ILinkResolver _linkResolver;

        private readonly IContentTypeRepository _contentTypeRepository;

        private readonly ContentMediaResolver _contentMediaResolver;

        public AssetHandler(
            IContentRepository contentRepository,
            IBlobFactory blobFactory,
            IContextResolver contextResolver,
            ILinkResolver linkResolver,
            IContentTypeRepository contentTypeRepository,
            ContentMediaResolver contentMediaResolver)
        {
            Guard.ValidateObject(contentRepository);
            Guard.ValidateObject(blobFactory);
            Guard.ValidateObject(contextResolver);
            Guard.ValidateObject(linkResolver);
            Guard.ValidateObject(contentTypeRepository);
            Guard.ValidateObject(contentMediaResolver);

            _contentRepository = contentRepository;
            _blobFactory = blobFactory;
            _contextResolver = contextResolver;
            _linkResolver = linkResolver;
            _contentTypeRepository = contentTypeRepository;
            _contentMediaResolver = contentMediaResolver;
        }

        public ContentReference InsertMediaByUrl<T>(ContentReference pageToStore, string fileName, string url, string imageExtension) where T : MediaData
        {
            if (
                ContentReference.IsNullOrEmpty(pageToStore)
                || string.IsNullOrEmpty(url)
                || string.IsNullOrEmpty(imageExtension))
            {
                return null;
            }

            var newImage = _contentRepository.GetDefault<T>(pageToStore);
            var blob = _blobFactory.CreateBlob(newImage.BinaryDataContainer, imageExtension);

            byte[] data;

            using (var webClient = new WebClient())
            {
                data = webClient.DownloadData(url);
            }

            using (var s = blob.OpenWrite())
            {
                var w = new StreamWriter(s);
                w.BaseStream.Write(data, 0, data.Length);
                w.Flush();
            }

            newImage.Name = fileName;
            newImage.BinaryData = blob;
            return _contentRepository.Save(newImage, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference InsertMediaByPath<T>(ContentReference pageToStore, string fileName, string filePath, string extension) where T : MediaData
        {
            if (
                ContentReference.IsNullOrEmpty(pageToStore)
                || string.IsNullOrEmpty(filePath)
                || string.IsNullOrEmpty(extension))
            {
                return null;
            }

            var newImage = _contentRepository.GetDefault<T>(pageToStore);
            var byteArrayData = File.ReadAllBytes(filePath);

            var blob = _blobFactory.CreateBlob(newImage.BinaryDataContainer, extension);
            using (var s = blob.OpenWrite())
            {
                var w = new StreamWriter(s);
                w.BaseStream.Write(byteArrayData, 0, byteArrayData.Length);
                w.Flush();
            }

            newImage.Name = fileName;
            newImage.BinaryData = blob;
            return _contentRepository.Save(newImage, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentReference InsertMediaByPath(ContentFolder parentFolder, string fileName, string filePath, int displayWidth, int displayHeight, string alternateText)
        {
            if (ContentReference.IsNullOrEmpty(parentFolder.ContentLink) || string.IsNullOrEmpty(filePath))
            {
                return null;
            }

            var existingImageFile = TryGetMedia<ImageFile>(fileName, parentFolder);

            var imageFile = existingImageFile != null
                                ? _contentRepository.Get<ImageFile>(existingImageFile).CreateWritableClone() as
                                      ImageFile
                                : _contentRepository.GetDefault<ImageFile>(parentFolder.ContentLink);

            if (imageFile == null)
            {
                throw new Exception();
            }

            var byteArrayData = File.ReadAllBytes(filePath);

            var blob = _blobFactory.CreateBlob(imageFile.BinaryDataContainer, Path.GetExtension(fileName));

            using (var s = blob.OpenWrite())
            {
                var w = new StreamWriter(s);
                w.BaseStream.Write(byteArrayData, 0, byteArrayData.Length);
                w.Flush();
            }

            imageFile.Name = fileName;
            imageFile.BinaryData = blob;
            imageFile.DisplayWidth = displayWidth;
            imageFile.DisplayHeight = displayHeight;
            imageFile.AlternativeText = alternateText;

            return _contentRepository.Save(imageFile, SaveAction.Publish, AccessLevel.NoAccess);
        }

        public ContentFolder CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                return null;
            }

            var returnValue = GetFolderByPath(directoryPath);

            if (returnValue != null)
            {
                return returnValue;
            }

            var directoryParts = directoryPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var parentFolder = _contextResolver.SiteAssetsFolder;

            foreach (var directoryPart in directoryParts)
            {
                returnValue =
                    parentFolder =
                        GetFolderByPath(directoryPart, parentFolder)
                        ?? CreateDirectory(directoryPart, parentFolder);
            }

            return returnValue;
        }

        public ContentFolder CreateDirectory(string folderName, ContentFolder parentFolder)
        {
            if (folderName.Contains("/"))
            {
                return null;
            }

            var contentFile =
                _contentRepository.GetDefault<ContentFolder>(
                    GetFolderReference(parentFolder));

            var contentFolders = _contentRepository.GetChildren<ContentFolder>(parentFolder.ParentLink);
            
            if (contentFolders != null && contentFolders.Any(x => x.Name == folderName))
            {
                return contentFolders.FirstOrDefault(x => x.Name == folderName);
            }

            contentFile.Name = folderName;

            try
            {
                var contentReference = _contentRepository.Save(
                    contentFile,
                    SaveAction.Publish,
                    AccessLevel.NoAccess);

                return _contentRepository.Get<ContentFolder>(contentReference);
            }
            catch (Exception e)
            {
                Log.Warn($"Create Folder by Name failed for {folderName} on parent {parentFolder.RouteSegment}", e);
            }

            return null;
        }

        public ContentReference TryGetMedia<T>(string fileName, ContentFolder parentFolder) where T : MediaData
        {
            try
            {
                var firstOrDefault =
                    _contentRepository.GetChildren<T>(
                            GetFolderReference(parentFolder))
                        .FirstOrDefault(
                            x =>
                                string.Equals(
                                    x.Name,
                                    Path.GetFileName(fileName),
                                    StringComparison.CurrentCultureIgnoreCase));

                return firstOrDefault?.ContentLink;
            }
            catch (Exception e)
            {
                Log.Error($"Could not validate file {fileName} on folder {parentFolder.RouteSegment}", e);
            }

            return null;
        }

        public string[] GetPdfUrlsWithinDirectory(ContentFolder parentFolder)
        {
            if (parentFolder == null)
            {
                return new string[0];
            }

            try
            {
                var pdfFiles =
                    _contentRepository
                    .GetChildren<PdfFile>(
                        GetFolderReference(parentFolder));

                return pdfFiles.Select(pdfFile => _linkResolver.GetFriendlyUrl(pdfFile.ContentLink)).ToArray();
            }
            catch (Exception e)
            {
                Log.Error($"Could not retrieve Pdf file Urls from {parentFolder.RouteSegment}", e);
            }

            return new string[0];
        }

        public ContentFolder GetFolderByPath(string directoryPath)
        {
            if (directoryPath == null)
            {
                return null;
            }

            var directoryParts = directoryPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return GetFolderByPath(directoryParts, _contextResolver.SiteAssetsFolder);
        }

        public ContentFolder GetFolderByPath(string[] pathElements, ContentFolder rootFolder)
        {
            ContentFolder findMediaFolderByName = null;
            var parentFolder = rootFolder;
            foreach (var path in pathElements)
            {
                findMediaFolderByName = GetFolderByPath(path, parentFolder);
                if (findMediaFolderByName == null)
                {
                    return null;
                }

                parentFolder = findMediaFolderByName;
            }

            return findMediaFolderByName;
        }

        public ContentFolder GetOrCreateFolderByPath(string directoryPath)
        {
            return GetFolderByPath(directoryPath) ?? CreateDirectory(directoryPath);
        }

        public ContentReference UploadPdf(byte[] fileContent, string fileName, ContentFolder parentFolder)
        {
            try
            {
                PdfFile pdfFile;

                var file = TryGetMedia<PdfFile>(fileName, parentFolder);

                if (file != null)
                {
                    pdfFile =
                        _contentRepository.Get<PdfFile>(file).CreateWritableClone() as
                            PdfFile;
                }
                else
                {
                    var fileExtension = Path.GetExtension(fileName);
                    var mediaType = _contentMediaResolver.GetFirstMatching(fileExtension);
                    var contentType = _contentTypeRepository.Load(mediaType);

                    var contentReference = GetFolderReference(parentFolder);

                    pdfFile = _contentRepository.GetDefault<PdfFile>(
                        contentReference,
                        contentType.ID);
                    pdfFile.Name = fileName;
                }

                if (pdfFile == null)
                {
                    throw new Exception();
                }

                pdfFile.BinaryData = _blobFactory.CreateBlob(pdfFile.BinaryDataContainer, Path.GetExtension(fileName));
                var stream = new MemoryStream(fileContent);
                pdfFile.BinaryData.Write(stream);

                return _contentRepository.Save(pdfFile, SaveAction.Publish, AccessLevel.Read);
            }
            catch (Exception e)
            {
                Log.Warn(
                    $"Failed to upload file {fileName} to folder {parentFolder.RouteSegment} id {parentFolder.ContentLink.ID}",
                    e);
            }

            return null;
        }

        private ContentFolder GetFolderByPath(string name, ContentFolder parentFolder)
        {
            if (
                name == GlobalConstants.Keys.SiteAssetsFolderName
                && ContentReference.IsNullOrEmpty(parentFolder.ContentLink))
            {
                return _contextResolver.SiteAssetsFolder;
            }

            var subDirectories =
                _contentRepository.GetChildren<ContentFolder>(
                    GetFolderReference(parentFolder));

            return
                subDirectories.FirstOrDefault(
                    x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        private ContentReference GetFolderReference(ContentFolder parentFolder)
        {
            if (parentFolder == null)
            {
                return null;
            }

            return ContentReference.IsNullOrEmpty(parentFolder.ContentLink)
                ? parentFolder.ParentLink
                : parentFolder.ContentLink;
        }
    }
}
