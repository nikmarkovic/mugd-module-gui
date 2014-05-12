using System;
using Assets.Scripts.Core.Gui.Types;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Elements
{
    [Serializable]
    public class GuiTransform
    {
        [SerializeField] private bool _autoSize;
        private Parent _parent;
        [SerializeField] private Position _position;
        private Rect _rect;
        [SerializeField] private float _rotation;
        [SerializeField] private Scale _scale;

        public GuiTransform()
        {
            _parent = Parent.Null();
        }

        public Position Position
        {
            get { return _position; }
            set
            {
                _position = value;
                Refresh();
            }
        }

        public Scale Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                Refresh();
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value%360 < 0 ? 360 + value%360 : value%360; }
        }

        public bool AutoSize
        {
            get { return _autoSize; }
            set { _autoSize = value; }
        }

        public Parent Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                Refresh();
            }
        }

        public Rect Rect
        {
            get { return _rect; }
            set
            {
                var x = _position.X.Unit == Number.Units.Percentage
                    ? value.x.Pixel().ToPercentage((_parent.Width - value.width))
                    : value.x.Pixel();
                var y = _position.Y.Unit == Number.Units.Percentage
                    ? value.y.Pixel().ToPercentage((_parent.Height - value.height))
                    : value.y.Pixel();
                var z = _position.Z;

                var width = _scale.Width.Unit == Number.Units.Percentage
                    ? value.width.Pixel().ToPercentage(_parent.Width)
                    : value.width.Pixel();
                var height = _scale.Height.Unit == Number.Units.Percentage
                    ? value.height.Pixel().ToPercentage(_parent.Height)
                    : value.height.Pixel();
                _position = Position.ValueOf(x, y, z);
                _scale = Scale.ValueOf(width, height, _scale.Ratio);
                Refresh();
            }
        }

        public void Refresh()
        {
            var width = _scale.Width.ToPixel(_parent.Width);
            if (!0f.Equals(_scale.Ratio))
            {
                _scale = _scale.SetHeight(_scale.Height.Unit == Number.Units.Pixel
                    ? (width.Value*_scale.Ratio).Pixel()
                    : (width.Value*_scale.Ratio).Pixel().ToPercentage(_parent.Height));
            }
            var height = _scale.Height.ToPixel(_parent.Height);
            var x = _position.X.ToPixel(_parent.Width - width.Value);
            var y = _position.Y.ToPixel(_parent.Height - height.Value);
            _rect = new Rect(x.Value, y.Value, width.Value, height.Value);
        }
    }
}