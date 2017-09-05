namespace JonDJones.Website.Core.EpiserverConfiguration.PropertyList
{
    using EPiServer.Core;
    using EPiServer.PlugIn;

    using Newtonsoft.Json;

    using JonDJones.Website.Core.Entities;

    [PropertyDefinitionTypePlugIn]
    public class AnchorLinkItemPropertyList : PropertyList<AnchorLinkItem>
    {
        public override PropertyData ParseToObject(string value)
        {
            ParseToSelf(value);
            return this;
        }

        protected override AnchorLinkItem ParseItem(string value)
        {
            return JsonConvert.DeserializeObject<AnchorLinkItem>(value);
        }
    }
}