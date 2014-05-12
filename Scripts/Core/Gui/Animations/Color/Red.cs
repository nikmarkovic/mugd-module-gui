using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;

namespace Assets.Scripts.Core.Gui.Animations.Color
{
    public class Red : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly float _value;
        private float _speed = 1;

        public Red(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value;
            _multiplicator = _value < GuiComponent.Style.Red ? -1 : 1;
            AnimationEnd += component => component.Style.Red = _value;
        }

        public Red Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return _multiplicator == -1 ? GuiComponent.Style.Red <= _value : GuiComponent.Style.Red >= _value;
        }

        protected override void UpdateAnimation()
        {
            GuiComponent.Style.Red += (_speed*_multiplicator)/500f;
        }
    }
}