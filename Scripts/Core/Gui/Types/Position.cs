using System;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Types
{
    [Serializable]
    public class Position
    {
        [SerializeField] private Number _x = 50f.Percentage();
        [SerializeField] private Number _y = 50f.Percentage();
        [SerializeField] private int _z;

        private Position(Number x, Number y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public Number X
        {
            get { return _x; }
        }

        public Number Y
        {
            get { return _y; }
        }

        public int Z
        {
            get { return _z; }
        }

        public static Position ValueOf(Number x, Number y, int z)
        {
            return new Position(x, y, z);
        }

        public static Position Pixels(float x, float y, int z)
        {
            return new Position(x.Pixel(), y.Pixel(), z);
        }

        public static Position Percentage(float x, float y, int z)
        {
            return new Position(x.Percentage(), y.Percentage(), z);
        }

        public static Position Copy(Position value)
        {
            return value.Copy();
        }

        public Position Copy()
        {
            return new Position(_x, _y, _z);
        }

        public Position SetX(Number value)
        {
            return new Position(value, _y, _z);
        }

        public Position SetY(Number value)
        {
            return new Position(_x, value, _z);
        }

        public Position SetZ(int value)
        {
            return new Position(_x, _y, value);
        }
    }
}