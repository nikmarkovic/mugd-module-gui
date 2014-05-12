using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;
using Assets.Scripts.Core.Gui.Types;

namespace Assets.Scripts.Core.Gui.Animations.Transform
{
    public class ScaleHeight : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly Number.Units _unit;
        private readonly float _value;
        private float _speed = 1;

        public ScaleHeight(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _unit = GuiComponent.Transform.Scale.Height.Unit;
            _multiplicator = _value < GuiComponent.Transform.Scale.Height.Value ? -1 : 1;
            AnimationEnd +=
                component => component.Transform.Scale =
                    component.Transform.Scale.SetHeight(_unit == Number.Units.Percentage
                        ? _value.Percentage()
                        : _value.Pixel());
        }

        public ScaleHeight Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1
                ? GuiComponent.Transform.Scale.Height.Value <= _value
                : GuiComponent.Transform.Scale.Height.Value >= _value;
        }

        protected override void UpdateAnimation()
        {
            var value = GuiComponent.Transform.Scale.Height.Value + (_speed*_multiplicator)/10f;
            GuiComponent.Transform.Scale =
                GuiComponent.Transform.Scale.SetHeight(_unit == Number.Units.Percentage
                    ? value.Percentage()
                    : value.Pixel());
        }
    }
}