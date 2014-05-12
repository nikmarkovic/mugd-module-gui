using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;
using UnityEngine;

namespace Assets.Scripts.Core.Gui.Animations.Content
{
    public class Content : GuiAnimation
    {
        private readonly float _fps;
        private readonly float _startTime;
        private Func<bool> _customEndCondition;
        private float _duration;
        protected bool _end;
        private float _lastFrameTime;
        protected bool _loop = true;

        public Content(AbstractComponent guiComponent, List<GuiAnimation> animations, float fps)
            : base(guiComponent, animations)
        {
            _fps = fps;
            _startTime = Time.time;
        }

        public Content Loop()
        {
            _loop = true;
            return this;
        }

        public Content DoNotLoop()
        {
            _loop = false;
            return this;
        }

        public Content Duration(float value)
        {
            _duration = value;
            return this;
        }

        public Content EndCondition(Func<bool> value)
        {
            _customEndCondition = value;
            return this;
        }

        protected override bool End()
        {
            return _end;
        }

        protected override void UpdateAnimation()
        {
            _end = (!0f.Equals(_duration) && (Time.time - _startTime > _duration)) ||
                   (_customEndCondition != null && _customEndCondition());
            if (!(Time.time - _lastFrameTime >= 1/_fps)) return;
            _lastFrameTime = Time.time;
            Animate();
        }

        protected virtual void Animate()
        {
            if (GuiComponent.Content.CurrentIndex + 1 < GuiComponent.Content.Contents.Length)
            {
                ++GuiComponent.Content.CurrentIndex;
            }
            else if (_loop)
            {
                GuiComponent.Content.CurrentIndex = 0;
            }
            else
            {
                _end = true;
            }
        }
    }
}