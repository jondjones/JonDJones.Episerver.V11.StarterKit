namespace JonDJones.Website.Core.Validation.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;

    using EPiServer.Core;
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class MinItemsAttribute : ValidationAttribute
    {
        private readonly int _minimumItem;

        public MinItemsAttribute(int minimumItem)
        {
            _minimumItem = minimumItem;
        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, ErrorMessage, name, _minimumItem);
        }

        public override bool IsValid(object value)
        {
            return ValidateContentArea(value as ContentArea);
        }

        private bool ValidateContentArea(ContentArea contentArea)
        {
            return contentArea?.Items == null || !contentArea.Items.Any() || contentArea.Items.Count >= _minimumItem;
        }
    }
}