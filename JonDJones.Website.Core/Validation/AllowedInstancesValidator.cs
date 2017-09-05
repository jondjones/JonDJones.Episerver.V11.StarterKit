namespace JonDJones.Website.Core.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using EPiServer;
    using EPiServer.Core;
    using EPiServer.Validation;

    using JonDJones.Website.Core.Validation.Attributes;
    using JonDJones.Website.Shared.Helpers;
    public class AllowedInstancesValidator : IValidate<PageData>
    {
        private readonly IContentLoader _contentLoader;

        public AllowedInstancesValidator(IContentLoader contentLoader)
        {
            Guard.ValidateObject(contentLoader);
            _contentLoader = contentLoader;
        }
        
        public IEnumerable<ValidationError> Validate(PageData instance)
        {
            var allowedInstanceAttribute = instance.GetType().GetCustomAttribute<AllowedInstancesAttribute>(true);

            if (allowedInstanceAttribute == null)
            {
                return Enumerable.Empty<ValidationError>();
            }

            var searchRoot = allowedInstanceAttribute.Scope == AllowedInstancesAttribute.InstanceScope.Site
                                 ? !PageReference.IsNullOrEmpty(ContentReference.StartPage) ? ContentReference.StartPage : instance.ParentLink
                                 : instance.ParentLink;

            var instancesOfType = GetInstancesOfType(instance.GetType(), searchRoot, allowedInstanceAttribute.Scope);

            if (instance.PendingPublish && !instance.IsModified)
            {
                instancesOfType++;
            }

            if (instancesOfType > allowedInstanceAttribute.MaxInstances)
            {
                return new[]
                           {
                               new ValidationError
                                   {
                                       ErrorMessage =
                                           $"Only {allowedInstanceAttribute.MaxInstances} instances of this page type can exist at this level",
                                       PropertyName = "PageName",
                                       Severity = ValidationErrorSeverity.Error,
                                       ValidationType = ValidationErrorType.StorageValidation
                                   }
                           };
            }

            return Enumerable.Empty<ValidationError>();
        }

        private int GetInstancesOfType(
            Type type,
            ContentReference rootPage,
            AllowedInstancesAttribute.InstanceScope instanceScope)
        {
            var instances = 0;
            var children = _contentLoader.GetChildren<IContent>(rootPage, new LoaderOptions());

            foreach (var child in children)
            {
                if (type == child.GetType())
                {
                    instances++;
                }

                if (instanceScope == AllowedInstancesAttribute.InstanceScope.SameParent)
                {
                    continue;
                }

                instances += GetInstancesOfType(type, child.ContentLink, instanceScope);
            }

            return instances;
        }
    }
}