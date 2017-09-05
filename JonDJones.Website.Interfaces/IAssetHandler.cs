namespace JonDJones.Website.Interfaces
{
    using EPiServer.Core;

    public interface IAssetHandler
    {
        ContentReference InsertMediaByUrl<T>(ContentReference pageToStore, string fileName, string url, string imageExtension) where T : MediaData;

        ContentReference InsertMediaByPath<T>(ContentReference pageToStore, string fileName, string filePath, string extension) where T : MediaData;

        ContentReference InsertMediaByPath(ContentFolder parentFolder, string fileName, string filePath, int displayWidth = 0, int displayHeight = 0, string alternateText = "");

        ContentReference UploadPdf(byte[] fileContent, string fileName, ContentFolder parentFolder);

        ContentReference TryGetMedia<T>(string fileName, ContentFolder parentFolder) where T : MediaData;

        ContentFolder CreateDirectory(string folderName);

        ContentFolder GetFolderByPath(string directoryPath);

        ContentFolder GetOrCreateFolderByPath(string directoryPath);

        ContentFolder CreateDirectory(string folderName, ContentFolder parentFolder);

        ContentFolder GetFolderByPath(string[] pathElements, ContentFolder parentFolder);

        string[] GetPdfUrlsWithinDirectory(ContentFolder pareFolder);
    }
}
