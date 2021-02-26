using System;

namespace DevConsole
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        public string Alias { get; }

        public CommandAttribute (string alias = null)
        {
            Alias = alias;
        }
    }
}