namespace JonDJones.Website.Interfaces
{
    using System.Collections.Generic;

    public interface INavigationItem
    {
        string ImageUrl { get; set; }

        string Link { get; set; }

        string Name { get; set; }

        string SubMenuTitle { get; set; }

        IEnumerable<INavigationItem> SubMenuItems { get; set; }
    }
}