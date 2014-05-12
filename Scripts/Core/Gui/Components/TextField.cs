using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof (GuiPanel))]
    [AddComponentMenu("Mugd/Gui/TextField")]
    public class TextField : GuiComponent
    {
        [SerializeField] private int _maxLength;
        [SerializeField] private bool _multiline;
        [SerializeField] private string _text;

        public bool Multiline
        {
            get { return _multiline; }
            set { _multiline = value; }
        }

        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == _text) return;
                _text = value;
                CallEvent(new TextFieldEventArgs(_text));
            }
        }

        protected override void DrawElement()
        {
            Text = _maxLength == 0
                ? _multiline
                    ? GUI.TextArea(Transform.Rect, _text, Style.Current)
                    : GUI.TextField(Transform.Rect, _text, Style.Current)
                : _multiline
                    ? GUI.TextArea(Transform.Rect, _text, _maxLength, Style.Current)
                    : GUI.TextField(Transform.Rect, _text, _maxLength, Style.Current);
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return _multiline ? new GUIStyle(GUI.skin.textArea) : new GUIStyle(GUI.skin.textField);
        }

        public class TextFieldEventArgs : EventArgs
        {
            private readonly string _text;

            public TextFieldEventArgs(string text)
            {
                _text = text;
            }

            public string Text
            {
                get { return _text; }
            }
        }
    }
}