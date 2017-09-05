namespace JonDJones.Website.Core.EpiserverConfiguration.PropertyList
{
    using EPiServer.Core;
    using EPiServer.PlugIn;

    using Newtonsoft.Json;

    using JonDJones.Website.Core.Entities;

    [PropertyDefinitionTypePlugIn]
    public class OrderedListItemPropertyList : PropertyList<OrderedListItem>
    {
        public override PropertyData ParseToObject(string value)
        {
            ParseToSelf(value);
            return this;
        }

        protected override OrderedListItem ParseItem(string value)
        {
            return JsonConvert.DeserializeObject<OrderedListItem>(value);
        }
    }
}