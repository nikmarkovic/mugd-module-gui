using UnityEngine;

namespace Assets.Scripts.Core.Gui.Components
{
    [RequireComponent(typeof(GuiPanel))]
    [AddComponentMenu("Mugd/Gui/Label")]
    public class Label : GuiComponent
    {
        protected override void DrawElement()
        {
            GUI.Label(Transform.Rect, Content.Current, Style.Current);
        }

        protected override GUIStyle DefaultGuiStyle()
        {
            return new GUIStyle(GUI.skin.label);
        }
    }
}