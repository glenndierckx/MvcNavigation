using System;
using System.Reflection;
using System.Resources;

namespace Glnn.MvcNavigation.Helpers
{
    public static class ResourceHelper
    {
        /// <summary>
        /// Gets the specified format.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "GlennD")]
        public static string Get<T>(string format, params object[] args)
            where T : class
        {
            var type = typeof(T);
            return Get(type, string.Format(format, args));
        }

        public static string Get(Type resourceType, string field)
        {
            // Create the resource manager.
            Assembly assembly = resourceType.Assembly;

            //ResFile.Strings -> <Namespace>.<ResourceFileName i.e. Strings.resx> 
            var resman = new ResourceManager(resourceType.FullName, assembly);

            // Load the value of string value for Client
            return resman.GetString(field);
        }
    }
}
