namespace JonDJones.Website.Core.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using EPiServer;
    using EPiServer.Cms.Shell;
    using EPiServer.ServiceLocation;
    using EPiServer.Shell.ObjectEditing;
    using EPiServer.Web;
    using EPiServer.Web.Routing;

    using JonDJones.Website.Core.Entities.Base;
    using JonDJones.Website.Core.EpiserverConfiguration.Media;
    using JonDJones.Website.Interfaces;

    public class ImageThumbnailExtenderAttribute : Attribute, IMetadataAware
    {
        public Type CollectionType { get; set; }
        
        public Injected<TemplateResolver> TemplateResolver { get; set; }

        public Injected<UrlResolver> UrlResolver { get; set; }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            var extendedMetadata = (ExtendedMetadata)metadata;

            var generic = typeof(PropertyListBase<>);
            var constructed = generic.MakeGenericType(CollectionType);

            if (metadata.Model.GetType().BaseType == constructed)
            {
                dynamic model = metadata.Model;

                var images = model.List as IEnumerable<IHasImage>;

                if (images != null)
                {
                    var collection =
                        images.Where(hasImage => hasImage.Image != null)
                            .Select(
                                image =>
                                    new
                                        {
                                            id = image.Image.ID,
                                            imageUrl =
                                            ServiceLocator.Current.GetInstance<IContentRepository>()
                                                .Get<ImageFile>(image.Image)
                                                .PreviewUrl(UrlResolver.Service, TemplateResolver.Service)
                                        });

                    extendedMetadata.EditorConfiguration.Add("mappedImages", collection);
                }
            }
        }
    }
}