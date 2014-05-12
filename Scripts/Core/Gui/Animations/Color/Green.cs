using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;

namespace Assets.Scripts.Core.Gui.Animations.Color
{
    public class Green : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly float _value;
        private float _speed = 1;

        public Green(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _multiplicator = _value < GuiComponent.Style.Green ? -1 : 1;
            AnimationEnd += component => component.Style.Green = _value;
        }

        public Green Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1 ? GuiComponent.Style.Green <= _value : GuiComponent.Style.Green >= _value;
        }

        protected override void UpdateAnimation()
        {
            GuiComponent.Style.Green += (_speed*_multiplicator)/500f;
        }
    }
}