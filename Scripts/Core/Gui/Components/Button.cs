using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof (GuiPanel))]
    [AddComponentMenu("Mugd/Gui/Button")]
    public class Button : GuiComponent
    {
        [SerializeField] private bool _repeat;

        public bool Repeat
        {
            get { return _repeat; }
            set { _repeat = value; }
        }

        protected override void DrawElement()
        {
            if (_repeat
                ? GUI.RepeatButton(Transform.Rect, Content.Current, Style.Current)
                : GUI.Button(Transform.Rect, Content.Current, Style.Current))
                CallEvent(new EventArgs());
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return new GUIStyle(GUI.skin.button);
        }
    }
}