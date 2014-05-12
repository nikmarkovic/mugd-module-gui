using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Types
{
    public struct Parent : IEquatable<Parent>
    {
        private readonly float _width;
        private readonly float _height;

        private Parent(float width, float height)
        {
            _width = width;
            _height = height;
        }

        public float Width
        {
            get { return _width; }
        }

        public float Height
        {
            get { return _height; }
        }

        public bool Equals(Parent other)
        {
            return _height.Equals(other._height) && _width.Equals(other._width);
        }

        public static Parent ValueOf(float width, float height)
        {
            return new Parent(width, height);
        }

        public static Parent Null()
        {
            return new Parent(Screen.width, Screen.height);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Parent && Equals((Parent) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_height.GetHashCode()*397) ^ _width.GetHashCode();
            }
        }

        public static bool operator ==(Parent left, Parent right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Parent left, Parent right)
        {
            return !left.Equals(right);
        }
    }
}