namespace JonDJones.Website.Core.SelectionFactories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using EPiServer.Shell.ObjectEditing;

    public class EnumSelectionFactory<T> : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return Enum.GetValues(typeof(T)).Cast<object>().Select(value =>
            {
                var selectItem = new SelectItem
                {
                    Text = GetValueName(value).ToString(),
                    Value = value.ToString()
                };

                return selectItem;
            });
        }

        public string GetValueName(object value)
        {
            var str = Enum.GetName(typeof(T), value);
            var member = value.GetType().GetMember(value.ToString());
            if (member.Length <= 0)
            {
                return str;
            }

            var customAttributes = member[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            if (customAttributes.Length > 0)
            {
                str = GetDescriptionValue(customAttributes);
            }

            return str;
        }

        private static string GetDescriptionValue(object[] attributes)
        {
            return ((DisplayAttribute)attributes[0]).Name;
        }
    }
}
