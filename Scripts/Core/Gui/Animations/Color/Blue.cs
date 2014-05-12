using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;

namespace Assets.Scripts.Core.Gui.Animations.Color
{
    public class Blue : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly float _value;
        private float _speed = 1;

        public Blue(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _multiplicator = _value < GuiComponent.Style.Blue ? -1 : 1;
            AnimationEnd += component => component.Style.Blue = _value;
        }

        public Blue Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1 ? GuiComponent.Style.Blue <= _value : GuiComponent.Style.Blue >= _value;
        }

        protected override void UpdateAnimation()
        {
            GuiComponent.Style.Blue += (_speed*_multiplicator)/500f;
        }
    }
}