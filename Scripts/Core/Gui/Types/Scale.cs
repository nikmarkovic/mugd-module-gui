using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Types
{
    [Serializable]
    public class Scale
    {
        [SerializeField] private Number _height = 50f.Percentage();
        [SerializeField] private float _ratio;
        [SerializeField] private Number _width = 50f.Percentage();

        private Scale(Number width, Number height, float ratio)
        {
            _width = width;
            _height = height;
            _ratio = ratio;
        }

        public Number Width
        {
            get { return _width; }
        }

        public Number Height
        {
            get { return _height; }
        }

        public float Ratio
        {
            get { return _ratio; }
        }

        public static Scale ValueOf(Number width, Number height, float ratio)
        {
            return new Scale(width, height, ratio);
        }

        public static Scale Pixels(float width, float height, float ratio)
        {
            return new Scale(width.Pixel(), height.Pixel(), ratio);
        }

        public static Scale Percentage(float width, float height, float ratio)
        {
            return new Scale(width.Percentage(), height.Percentage(), ratio);
        }

        public static Scale Copy(Scale value)
        {
            return value.Copy();
        }

        public Scale Copy()
        {
            return new Scale(_width, _height, _ratio);
        }

        public Scale SetWidth(Number value)
        {
            return new Scale(value, _height, _ratio);
        }

        public Scale SetHeight(Number value)
        {
            return new Scale(_width, value, _ratio);
        }

        public Scale SetRatio(float value)
        {
            return new Scale(_width, _height, value);
        }
    }
}