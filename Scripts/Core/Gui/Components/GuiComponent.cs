using System;
using System.Linq;
using Assets.Scripts.Core.Gui.Attributes;
using Assets.Scripts.Core.Gui.Types;

namespace Assets.Scripts.Core.Gui.Components
{
    public abstract class GuiComponent : AbstractComponent
    {
        public delegate void EventHandler(object sender, EventArgs args);

        public GuiPanel Panel { get; private set; }

        public event EventHandler ComponenteEvent = delegate { };

        protected override void OnStart()
        {
            Panel = GetComponent<GuiPanel>();
            InjectMethod();
        }

        protected override void OnGuiUpdate()
        {
            Transform.Parent = Parent.ValueOf(Panel.Transform.Rect.width, Panel.Transform.Rect.height);
        }

        protected void CallEvent(EventArgs args)
        {
            ComponenteEvent(this, args);
        }

        private void InjectMethod()
        {
            Panel.GetType()
                .GetMethods()
                .Where(method => method.GetCustomAttributes(typeof (EventAttribute), true)
                    .Where(attribute =>
                    {
                        var eventAttribute = attribute as EventAttribute;
                        return eventAttribute != null && eventAttribute.Id == Id;
                    }).Any())
                .Where(method => method.ReturnType == typeof (void)
                                 && method.GetParameters().Length == 2
                                 && method.GetParameters()[0].ParameterType == typeof (object)
                                 && method.GetParameters()[1].ParameterType == typeof (EventArgs))
                .ToList()
                .ForEach(
                    methodEvent =>
                    {
                        ComponenteEvent +=
                            Delegate.CreateDelegate(typeof (EventHandler), Panel, methodEvent) as EventHandler;
                    });
        }
    }
}