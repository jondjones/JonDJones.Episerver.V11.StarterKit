namespace JonDJones.Website.Core.Helpers
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    #endregion

    /// <summary>
    ///     Type Attribute Helper
    /// </summary>
    public class TypeAttributeHelper
    {
        #region Public Methods

        /// <summary>
        ///     Gets the types child of.
        /// </summary>
        /// <typeparam name="T">Generic Object</typeparam>
        /// <returns>List of assembly</returns>
        public static IEnumerable<Type> GetTypesChildOf<T>()
        {
            var types = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                types.AddRange(GetTypesChildOfInAssembly(typeof(T), assembly));
            }

            return types;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     Gets the assemblies.
        /// </summary>
        /// <returns>List of assembly</returns>
        private static IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        /// <summary>
        ///     Gets the types child of in assembly.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns>List of assembly</returns>
        private static IEnumerable<Type> GetTypesChildOfInAssembly(Type type, Assembly assembly)
        {
            try
            {
                return assembly.GetTypes().Where(t => type.IsAssignableFrom(t) && t.IsClass);
            }
            catch (Exception)
            {
                // there could be situations when type could not be loaded
                // this may happen if we are visiting *all* loaded assemblies in application domain
                return Enumerable.Empty<Type>();
            }
        }

        #endregion
    }
}