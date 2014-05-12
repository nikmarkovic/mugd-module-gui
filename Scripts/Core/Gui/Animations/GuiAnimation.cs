using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Gui.Animations.Color;
using Assets.Scripts.Core.Gui.Animations.Content;
using Assets.Scripts.Core.Gui.Animations.Transform;
using Assets.Scripts.Core.Gui.Components;

namespace Assets.Scripts.Core.Gui.Animations
{
    public class GuiAnimation
    {
        public delegate void AnimationEndEventHendler(AbstractComponent component);

        protected readonly AbstractComponent GuiComponent;
        private readonly List<GuiAnimation> _animations;
        private bool _canceled;

        public GuiAnimation(AbstractComponent guiComponent, List<GuiAnimation> animations)
        {
            GuiComponent = guiComponent;
            _animations = animations;
        }

        public List<GuiAnimation> Animations
        {
            get { return _animations; }
        }

        public bool IsPlaying
        {
            get { return _animations.Any(); }
        }

        public event AnimationEndEventHendler AnimationEnd = delegate { };

        public Alpha Alpha(float value)
        {
            var alpha = new Alpha(GuiComponent, _animations, value);
            _animations.Add(alpha);
            return alpha;
        }

        public Alpha AlphaBy(float value)
        {
            return Alpha(GuiComponent.Style.Alpha + value);
        }

        public Red Red(float value)
        {
            var red = new Red(GuiComponent, _animations, value);
            _animations.Add(red);
            return red;
        }

        public Red RedBy(float value)
        {
            return Red(GuiComponent.Style.Red + value);
        }

        public Green Green(float value)
        {
            var green = new Green(GuiComponent, _animations, value);
            _animations.Add(green);
            return green;
        }

        public Green GreenBy(float value)
        {
            return Green(GuiComponent.Style.Green + value);
        }

        public Blue Blue(float value)
        {
            var blue = new Blue(GuiComponent, _animations, value);
            _animations.Add(blue);
            return blue;
        }

        public Blue BlueBy(float value)
        {
            return Blue(GuiComponent.Style.Blue + value);
        }

        public Rotation Rotation(float value)
        {
            var rotation = new Rotation(GuiComponent, _animations, value);
            _animations.Add(rotation);
            return rotation;
        }

        public Rotation RotationBy(float value)
        {
            return Rotation(GuiComponent.Transform.Rotation + value);
        }

        public ScaleWidth ScaleWidth(float value)
        {
            var scale = new ScaleWidth(GuiComponent, _animations, value);
            _animations.Add(scale);
            return scale;
        }

        public ScaleWidth ScaleWidthBy(float value)
        {
            return ScaleWidth(GuiComponent.Transform.Scale.Width.Value + value);
        }

        public ScaleHeight ScaleHeight(float value)
        {
            var scale = new ScaleHeight(GuiComponent, _animations, value);
            _animations.Add(scale);
            return scale;
        }

        public ScaleHeight ScaleHeightBy(float value)
        {
            return ScaleHeight(GuiComponent.Transform.Scale.Height.Value + value);
        }

        public TranslateX TranslationX(float value)
        {
            var translation = new TranslateX(GuiComponent, _animations, value);
            _animations.Add(translation);
            return translation;
        }

        public TranslateX TranslationXBy(float value)
        {
            return TranslationX(GuiComponent.Transform.Position.X.Value + value);
        }

        public TranslateY TranslationY(float value)
        {
            var translation = new TranslateY(GuiComponent, _animations, value);
            _animations.Add(translation);
            return translation;
        }

        public TranslateY TranslationYBy(float value)
        {
            return TranslationY(GuiComponent.Transform.Position.Y.Value + value);
        }

        public Content.Content Content(float fps)
        {
            var content = new Content.Content(GuiComponent, _animations, fps);
            _animations.Add(content);
            return content;
        }

        public Style Style(float fps)
        {
            var style = new Style(GuiComponent, _animations, fps);
            _animations.Add(style);
            return style;
        }

        public void Cancel()
        {
            _canceled = true;
        }

        public void Update()
        {
            if (!IsPlaying && AnimationEnd != null)
            {
                AnimationEnd(GuiComponent);
                AnimationEnd = null;
            }
            if (_canceled)
            {
                _animations.Clear();
                _canceled = false;
            }
            _animations.ForEach(animation =>
            {
                if (animation.End() || animation._canceled)
                {
                    animation.AnimationEnd(GuiComponent);
                    _animations.Remove(animation);
                }
                else
                {
                    animation.UpdateAnimation();
                }
            });
        }

        protected virtual bool End()
        {
            return false;
        }

        protected virtual void UpdateAnimation()
        {
        }
    }
}