using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Dependency
{
    public class Message
    {
        public static string Value
        {
            get
            {
                try
                {
                    var optional = AssemblyLoadContext.Default.LoadFromAssemblyName(new System.Reflection.AssemblyName("OptionalSubDependency"));
                    var myType = optional.GetType("OptionalSubDependency.Optional");
                    var myInstance = Activator.CreateInstance(myType);
                    return (string)myInstance.GetType().GetField("Value").GetValue(myInstance);
                }
                catch
                {
                    return "Fallback!";
                }
            }
        }
    }
}
