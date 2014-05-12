using System.Collections.Generic;
using Assets.Scripts.Core.Gui.Components;

namespace Assets.Scripts.Core.Gui.Animations.Content
{
    public class Style : Content
    {
        public Style(AbstractComponent guiComponent, List<GuiAnimation> animations, float fps)
            : base(guiComponent, animations, fps)
        {
        }

        protected override void Animate()
        {
            if (GuiComponent.Style.CurrentIndex + 1 < GuiComponent.Style.Styles.Length)
            {
                ++GuiComponent.Style.CurrentIndex;
            }
            else if (_loop)
            {
                GuiComponent.Style.CurrentIndex = 0;
            }
            else
            {
                _end = true;
            }
        }
    }
}