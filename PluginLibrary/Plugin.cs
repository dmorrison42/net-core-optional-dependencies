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
        static List<Type> AllTypes = new List<Type>();
        static Type TargetType;
        static Plugin()
        {
            foreach (var reference in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                AllTypes.AddRange(Assembly.Load(reference).GetTypes());
            }

            var defaultType = typeof(DefaultPlugin);
            TargetType = AllTypes
                .Where(t => typeof(IPlugin).IsAssignableFrom(t))
                .Where(t => 
                    t.IsClass)
                .Where(t => 
                    t != defaultType)
                .FirstOrDefault();
        }

        public static IPlugin New()
        {
            if (TargetType != null)
            {
                return (IPlugin) Activator.CreateInstance(TargetType);
            }
            return new DefaultPlugin();
        }
    }
}
