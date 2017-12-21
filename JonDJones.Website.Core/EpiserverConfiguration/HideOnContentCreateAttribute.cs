using System;

namespace JonDJones.Website.Core.EpiserverConfiguration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class HideOnContentCreateAttribute : Attribute { }
}
