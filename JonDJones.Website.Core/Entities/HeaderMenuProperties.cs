namespace JonDJones.Website.Core.Entities
{
    #region Using

    using System.Collections.Generic;
    using System.Linq;

    using JonDJones.Website.Interfaces;

    #endregion

    /// <summary>
    ///     Header Menu Properties
    /// </summary>
    /// <seealso cref="IHeaderMenuProperties" />
    public class HeaderMenuProperties : IHeaderMenuProperties
    {
        #region Explicit Interface Events

        /// <summary>
        ///     Gets or sets the primary navigation items.
        /// </summary>
        /// <value>
        ///     The primary navigation items.
        /// </value>
        public List<INavigationItem> MenuItems { get; set; }

        /// <summary>
        ///     Gets or sets the footer navigation items.
        /// </summary>
        /// <value>
        ///     The footer navigation items.
        /// </value>
        public List<INavigationItem> TopNavigationItems { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has primary navigation items.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has primary navigation items; otherwise, <c>false</c>.
        /// </value>
        public bool HasPrimaryNavigationItems => MenuItems != null && MenuItems.Any();

        /// <summary>
        /// Gets a value indicating whether this instance has top navigation items.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has top navigation items; otherwise, <c>false</c>.
        /// </value>
        public bool HasTopNavigationItems => TopNavigationItems != null && TopNavigationItems.Any();

        #endregion
    }
}