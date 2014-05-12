using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof (GuiPanel))]
    [AddComponentMenu("Mugd/Gui/Toggle")]
    public class Toggle : GuiComponent
    {
        [SerializeField] private bool _toggle;

        public bool State
        {
            get { return _toggle; }
            set
            {
                if (value == _toggle) return;
                _toggle = value;
                CallEvent(new ToggleEventArgs(_toggle));
            }
        }

        protected override void DrawElement()
        {
            State = GUI.Toggle(Transform.Rect, _toggle, Content.Current, Style.Current);
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return new GUIStyle(GUI.skin.toggle);
        }

        public class ToggleEventArgs : EventArgs
        {
            private readonly bool _value;

            public ToggleEventArgs(bool value)
            {
                _value = value;
            }

            public bool Value
            {
                get { return _value; }
            }
        }
    }
}