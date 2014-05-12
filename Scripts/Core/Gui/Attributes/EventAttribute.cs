using System;

namespace Assets.Scripts.Core.Gui.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EventAttribute : Attribute
    {
        public readonly string Id;

        public EventAttribute(string id)
        {
            Id = id;
        }
    }
}