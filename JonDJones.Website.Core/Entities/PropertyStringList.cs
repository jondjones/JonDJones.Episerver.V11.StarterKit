namespace JonDJones.Website.Core.Entities
{
    using System;

    using EPiServer.Core;
    using EPiServer.PlugIn;
    using EPiServer.Web;
    [PropertyDefinitionTypePlugIn(Description = "A property for list of strings", DisplayName = "String List")]
    public class PropertyStringList : PropertyLongString
    {
        protected const string Separator = "\n";
        
        public string[] List => (string[])Value;

        public override Type PropertyValueType => typeof(string[]);

        public override object Value
        {
            get
            {
                var value = base.Value as string;
                return value?.Split(Separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }

            set
            {
                if (value is string[])
                {
                    var s = string.Join(Separator, value as string[]);
                    base.Value = s;
                }
                else
                {
                    base.Value = value;
                }
            }
        }
        
        public override object SaveData(PropertyDataCollection properties) => LongString;
    }
}