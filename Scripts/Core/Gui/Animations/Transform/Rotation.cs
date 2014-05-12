using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Animations.Transform
{
    public class Rotation : GuiAnimation
    {
        private readonly int _multiplicator;
        private readonly float _value;
        private float _speed = 1;

        public Rotation(AbstractComponent guiComponent, List<GuiAnimation> animations, float value)
            : base(guiComponent, animations)
        {
            _value = value%360 < 0 ? 360 + value%360 : value%360;
            _multiplicator = CalculateMultiplicator();
            AnimationEnd += component => component.Transform.Rotation = _value;
        }

        public Rotation Speed(float speed)
        {
            _speed = speed;
            return this;
        }

        protected override bool End()
        {
            return Mathf.Abs(GuiComponent.Transform.Rotation - _value) <= _speed/10f;
        }

        protected override void UpdateAnimation()
        {
            GuiComponent.Transform.Rotation += (_speed*_multiplicator)/10f;
        }

        private int CalculateMultiplicator()
        {
            if (Mathf.Abs(GuiComponent.Transform.Rotation - _value) <
                Mathf.Abs(GuiComponent.Transform.Rotation + 360 - _value))
            {
                return GuiComponent.Transform.Rotation < _value ? 1 : -1;
            }
            return GuiComponent.Transform.Rotation < _value ? -1 : 1;
        }
    }
}