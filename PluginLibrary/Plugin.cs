using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PluginLibrary
{
    public static class Plugin
    {
        // Requires assemblies to be loaded. LoadAssembliesOnStartup.Fody does a clever job.
        // Do as much caching as possible, Reflection can be heavy!
        static readonly List<Type> AllTypes = new List<Type>();
        static readonly Type[] PluginTypes;
        static Plugin()
        {
            foreach (var reference in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                try
                {
                    AllTypes.AddRange(Assembly.Load(reference).GetTypes());
                }
                catch (ReflectionTypeLoadException e)
                {
                    AllTypes.AddRange(e.Types);
                }
            }

            PluginTypes = AllTypes
                .Where(t => typeof(IPlugin).IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .ToArray();
        }

        private static Dictionary<string, Type> ResultCache = new Dictionary<string, Type>();
        public static IPlugin New()
        {
            if (!ResultCache.ContainsKey("")) {
                var defaultType = typeof(DefaultPlugin);
                ResultCache[""] = PluginTypes
                    .FirstOrDefault(t => t != defaultType);
            }
            if (ResultCache[""] != null) {
                return (IPlugin) Activator.CreateInstance(ResultCache[""]);
            }
            return null;
        }
    }
}
