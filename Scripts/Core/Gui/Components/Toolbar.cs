using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof (GuiPanel))]
    [AddComponentMenu("Mugd/Gui/Toolbar")]
    public class Toolbar : GuiComponent
    {
        private int _selected;

        public int Selected
        {
            get { return _selected; }
            set
            {
                if (value == _selected) return;
                _selected = value;
                CallEvent(new ToolbarEventArgs(_selected));
            }
        }

        protected override void DrawElement()
        {
            Selected = GUI.Toolbar(Transform.Rect, _selected, Content.Contents, Style.Current);
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return new GUIStyle(GUI.skin.button);
        }

        public class ToolbarEventArgs : EventArgs
        {
            private readonly int _selected;

            public ToolbarEventArgs(int selected)
            {
                _selected = selected;
            }

            public int Selected
            {
                get { return _selected; }
            }
        }
    }
}