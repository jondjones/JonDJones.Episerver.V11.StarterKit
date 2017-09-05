namespace JonDJones.Website.Core.EditorDescriptors
{
    using EPiServer.Shell.ObjectEditing.EditorDescriptors;

    using JonDJones.Website.Core.Resources;
    using JonDJones.Website.Shared.Resources;

    [EditorDescriptorRegistration(TargetType = typeof(string[]), UIHint = GlobalConstants.UIHints.StringList)]
    public class PropertyStringListEditorDescriptor : EditorDescriptor
    {
        public PropertyStringListEditorDescriptor()
        {
            ClientEditingClass = "PropertyStringList";
        }
    }
}