namespace JonDJones.Website.Interfaces
{
    using System.Collections.Generic;

    public interface IHeaderMenuProperties
    {
        List<INavigationItem> MenuItems { get; set; }

        bool HasPrimaryNavigationItems { get; }
    }
}