using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;
using Assets.Scripts.Core.Gui.Types;

namespace Assets.Scripts.Core.Gui.Animations.Transform
{
    public class TranslateX : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly Number.Units _unit;
        private readonly float _value;
        private float _speed = 1;

        public TranslateX(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _unit = GuiComponent.Transform.Position.X.Unit;
            _multiplicator = _value < GuiComponent.Transform.Position.X.Value ? -1 : 1;
            AnimationEnd +=
                component => component.Transform.Position =
                    component.Transform.Position.SetX(_unit == Number.Units.Percentage
                        ? _value.Percentage()
                        : _value.Pixel());
        }

        public TranslateX Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1
                ? GuiComponent.Transform.Position.X.Value <= _value
                : GuiComponent.Transform.Position.X.Value >= _value;
        }

        protected override void UpdateAnimation()
        {
            var value = GuiComponent.Transform.Position.X.Value + (_speed*_multiplicator)/10f;
            GuiComponent.Transform.Position =
                GuiComponent.Transform.Position.SetX(_unit == Number.Units.Percentage
                    ? value.Percentage()
                    : value.Pixel());
        }
    }
}