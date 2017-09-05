namespace JonDJones.Website.Core.Entities.Base
{
    #region Using

    using EPiServer.Core;
    using EPiServer.Framework.Serialization;
    using EPiServer.Framework.Serialization.Internal;
    using EPiServer.ServiceLocation;

    #endregion

    /// <summary>
    ///     Inherits the abstract class PropertyList, and handles the JSON serialization of the property
    /// </summary>
    /// <typeparam name="T">Generic Object</typeparam>
    /// <seealso cref="PropertyList{T}" />
    public class PropertyListBase<T> : PropertyList<T>
    {
        #region Fields

        /// <summary>
        ///     The _object serialiser
        /// </summary>
        private readonly IObjectSerializer objectSerializer;

        /// <summary>
        ///     The _object serialiser factory
        /// </summary>
        private Injected<ObjectSerializerFactory> objectSerializerFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initialises a new instance of the <see cref="PropertyListBase{T}" /> class.
        /// </summary>
        public PropertyListBase()
        {
            objectSerializer = objectSerializerFactory.Service.GetSerializer("application/json");
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Parses to object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Property Data</returns>
        public override PropertyData ParseToObject(string value)
        {
            ParseToSelf(value);

            return this;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///     Parses the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Property Data</returns>
        protected override T ParseItem(string value)
        {
            return objectSerializer.Deserialize<T>(value);
        }

        #endregion
    }
}