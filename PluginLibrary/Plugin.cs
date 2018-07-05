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

        public static IPlugin New()
        {
            var defaultType = typeof(DefaultPlugin);
            var targetType = PluginTypes
                .FirstOrDefault(t => t != defaultType);
            if (targetType != null)
            {
                return (IPlugin) Activator.CreateInstance(targetType);
            }
            return new DefaultPlugin();
        }
    }
}
