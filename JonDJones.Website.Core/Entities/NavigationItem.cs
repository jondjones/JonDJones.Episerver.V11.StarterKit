namespace JonDJones.Website.Core.Entities
{
    using System.Collections.Generic;

    using JonDJones.Website.Interfaces;

    public class NavigationItem : INavigationItem
    {
        public NavigationItem()
        {
            SubMenuItems = new List<NavigationItem>();
        }

        public string Name { get; set; }

        public string Link { get; set; }

        public string ImageUrl { get; set; }

        public string SubMenuTitle { get; set; }

        public IEnumerable<INavigationItem> SubMenuItems { get; set; }
    }
}
