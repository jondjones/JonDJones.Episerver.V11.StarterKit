namespace JonDJones.Website.Core.Validation.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    using EPiServer.Core;
    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class MaxItemsAttribute : ValidationAttribute
    {
        private readonly int _maximumItem;
        
        public MaxItemsAttribute(int maximumItem)
        {
            _maximumItem = maximumItem;
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, _maximumItem);
        }

        public override bool IsValid(object value)
        {
            return ValidateContentArea(value as ContentArea);
        }

        private bool ValidateContentArea(ContentArea contentArea)
        {
            return contentArea?.Items == null || !contentArea.Items.Any() || contentArea.Items.Count <= _maximumItem;
        }
    }
}