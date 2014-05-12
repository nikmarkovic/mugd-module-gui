using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;
using Assets.Scripts.Core.Gui.Types;

namespace Assets.Scripts.Core.Gui.Animations.Transform
{
    public class TranslateY : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly Number.Units _unit;
        private readonly float _value;
        private float _speed = 1;

        public TranslateY(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _unit = GuiComponent.Transform.Position.Y.Unit;
            _multiplicator = _value < GuiComponent.Transform.Position.Y.Value ? -1 : 1;
            AnimationEnd +=
               component => component.Transform.Position =
                   component.Transform.Position.SetY(_unit == Number.Units.Percentage
                       ? _value.Percentage()
                       : _value.Pixel());
        }

        public TranslateY Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1
                ? GuiComponent.Transform.Position.Y.Value <= _value
                : GuiComponent.Transform.Position.Y.Value >= _value;
        }

        protected override void UpdateAnimation()
        {
            var value = GuiComponent.Transform.Position.Y.Value + (_speed*_multiplicator)/10f;
            GuiComponent.Transform.Position =
                GuiComponent.Transform.Position.SetY(_unit == Number.Units.Percentage
                    ? value.Percentage()
                    : value.Pixel());
        }
    }
}