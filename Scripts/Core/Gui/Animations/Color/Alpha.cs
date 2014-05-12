using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;

namespace Assets.Scripts.Core.Gui.Animations.Color
{
    public class Alpha : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly float _value;
        private float _speed = 1;

        public Alpha(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _multiplicator = _value < GuiComponent.Style.Alpha ? -1 : 1;
            AnimationEnd += component => component.Style.Alpha = _value;
        }

        public Alpha Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1 ? GuiComponent.Style.Alpha <= _value : GuiComponent.Style.Alpha >= _value;
        }

        protected override void UpdateAnimation()
        {
            GuiComponent.Style.Alpha += (_speed*_multiplicator)/500f;
        }
    }
}