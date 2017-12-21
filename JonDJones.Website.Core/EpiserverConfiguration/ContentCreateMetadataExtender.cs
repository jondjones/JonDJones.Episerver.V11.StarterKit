using EPiServer.Shell.ObjectEditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonDJones.Website.Core.EpiserverConfiguration
{
    public class ContentCreateMetadataExtender : IMetadataExtender
    {
        public void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            var model = metadata.Model as EPiServer.Core.PageData;
            if (model != null && model.ContentLink.ID == 0)
            {
                foreach (ExtendedMetadata property in metadata.Properties)
                {
                    if (property.Attributes.OfType<HideOnContentCreateAttribute>().Any())
                    {
                        property.IsRequired = false;
                    }
                }
            }
        }
    }
}
