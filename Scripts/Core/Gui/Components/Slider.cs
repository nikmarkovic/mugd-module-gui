using System;
using Assets.Scripts.Core.Gui.Elements;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof (GuiPanel))]
    [AddComponentMenu("Mugd/Gui/Slider")]
    public class Slider : GuiComponent
    {
        [SerializeField] private Style _thumbStyle = new Style();
        [SerializeField] private float _maxValue = 1;
        [SerializeField] private float _minValue;
        [SerializeField] private float _value;
        [SerializeField] private bool _vertical;

        public bool Vertical
        {
            get { return _vertical; }
            set { _vertical = value; }
        }

        public float MinValue
        {
            get { return _minValue; }
            set
            {
                if (value >= _maxValue) return;
                _minValue = value;
                Value = _value;
            }
        }

        public float MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (value <= _minValue) return;
                _maxValue = value;
                Value = _value;
            }
        }

        public float Value
        {
            get { return _value; }
            set
            {
                var newValue = Mathf.Clamp(value, _minValue, _maxValue);
                if (newValue.Equals(_value)) return;
                _value = newValue;
                CallEvent(new SliderEventArgs(_value));
            }
        }

        public Style ThumbStyle
        {
            get { return _thumbStyle; }
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (_thumbStyle.Current != GUIStyle.none) return;
            _thumbStyle.Styles = new[]
            {_vertical ? new GUIStyle(GUI.skin.verticalSliderThumb) : new GUIStyle(GUI.skin.horizontalSliderThumb)};
        }

        protected override void DrawElement()
        {
            Value = _vertical
                ? GUI.VerticalSlider(Transform.Rect, _value, _minValue, _maxValue, Style.Current, _thumbStyle.Current)
                : GUI.HorizontalSlider(Transform.Rect, _value, _minValue, _maxValue, Style.Current, _thumbStyle.Current);
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return _vertical ? new GUIStyle(GUI.skin.verticalSlider) : new GUIStyle(GUI.skin.horizontalSlider);
        }

        public class SliderEventArgs : EventArgs
        {
            private readonly float _value;

            public SliderEventArgs(float value)
            {
                _value = value;
            }

            public float Value
            {
                get { return _value; }
            }
        }
    }
}