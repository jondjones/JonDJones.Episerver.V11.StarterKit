namespace JonDJones.Website.Core.Validation.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AllowedInstancesAttribute : Attribute
    {
        public AllowedInstancesAttribute(int maxInstances)
        {
            MaxInstances = maxInstances;
        }
        
        public enum InstanceScope
        {
            Site,
            SameParent,
            SameParentOrDescendant
        }
        public int MaxInstances { get; set; }

        public InstanceScope Scope { get; set; }
    }
}