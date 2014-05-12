using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof (GuiPanel))]
    [AddComponentMenu("Mugd/Gui/Box")]
    public class Box : GuiComponent
    {
        protected override void DrawElement()
        {
            GUI.Box(Transform.Rect, Content.Current, Style.Current);
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return new GUIStyle(GUI.skin.box);
        }
    }
}