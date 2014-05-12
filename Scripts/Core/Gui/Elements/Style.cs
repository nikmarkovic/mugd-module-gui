using System;
using System.Linq;
using Assets.Scripts.Core.Gui.Types;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Elements
{
    [Serializable]
    public class Style
    {
        [SerializeField] private float _alpha = 1;
        [SerializeField] private float _blue = 1;
        private int _current;
        [SerializeField] private float _green = 1;
        [SerializeField] private float _red = 1;
        [SerializeField] private GUIStyle[] _styles;

        public GUIStyle[] Styles
        {
            get { return _styles; }
            set { _styles = value; }
        }

        public float Red
        {
            get { return _red; }
            set { _red = value; }
        }

        public float Green
        {
            get { return _green; }
            set { _green = value; }
        }

        public float Blue
        {
            get { return _blue; }
            set { _blue = value; }
        }

        public float Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        public GUIStyle Current
        {
            get { return _styles == null || !_styles.Any() ? GUIStyle.none : _styles[_current]; }
        }

        public int CurrentIndex
        {
            get { return _current; }
            set
            {
                if (_styles == null || _styles.Length - 1 < Mathf.Max(0, value)) return;
                _current = Mathf.Max(0, value);
            }
        }

        public static GUIStyle FixSizes(GUIStyle style)
        {
            var stylePx = new GUIStyle(style);
            stylePx.border = stylePx.border.Dp();
            stylePx.padding = stylePx.padding.Dp();
            stylePx.margin = stylePx.margin.Dp();
            stylePx.overflow = stylePx.overflow.Dp();
            stylePx.contentOffset = stylePx.contentOffset.Dp();
            stylePx.fixedWidth = stylePx.fixedWidth.Dp();
            stylePx.fixedHeight = stylePx.fixedHeight.Dp();
            stylePx.fontSize = stylePx.fontSize.Dp();
            return stylePx;
        }
    }
}