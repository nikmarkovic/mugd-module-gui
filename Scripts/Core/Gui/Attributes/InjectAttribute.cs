using System;

namespace Assets.Scripts.Core.Gui.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}