using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Types
{
    [Serializable]
    public class Number : IEquatable<Number>
    {
        public enum Units
        {
            Pixel,
            Percentage
        }

        [SerializeField] private float _value;
        [SerializeField] private Units _unit;

        private Number(float value, Units unit)
        {
            _value = value;
            _unit = unit;
        }

        public float Value
        {
            get { return _value; }
        }

        public Units Unit
        {
            get { return _unit; }
        }

        public bool Equals(Number other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _unit == other._unit && _value.Equals(other._value);
        }

        public static Number Pixel(float value)
        {
            return new Number(value, Units.Pixel);
        }

        public static Number Percentage(float value)
        {
            return new Number(value, Units.Percentage);
        }

        public static Number Copy(Number value)
        {
            return value.Copy();
        }

        public Number Copy()
        {
            return new Number(_value, _unit);
        }

        public Number ToPixel(float relative)
        {
            return _unit == Units.Percentage ? (relative*_value/100f).Pixel() : _value.Pixel();
        }

        public Number ToPercentage(float relative)
        {
            if (0f.Equals(relative)) return 0f.Percentage();
            return _unit == Units.Pixel ? (100*_value/relative).Percentage() : _value.Percentage();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Number) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) _unit*397) ^ _value.GetHashCode();
            }
        }

        public static bool operator ==(Number left, Number right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Number left, Number right)
        {
            return !Equals(left, right);
        }
    }
}