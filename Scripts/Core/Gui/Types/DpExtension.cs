using UnityEngine;

namespace Assets.Scripts.Core.Gui.Types
{
    public static class DpExtension
    {
        public static float Dp(this float value)
        {
            return value*Multiplicator();
        }

        public static int Dp(this int value)
        {
            return Mathf.RoundToInt(value*Multiplicator());
        }

        public static RectOffset Dp(this RectOffset rectOffset)
        {
            return new RectOffset(rectOffset.left.Dp(), rectOffset.right.Dp(), rectOffset.top.Dp(),
                rectOffset.bottom.Dp());
        }

        public static Vector2 Dp(this Vector2 vector)
        {
            return new Vector2(vector.x.Dp(), vector.y.Dp());
        }

        private static float Multiplicator()
        {
            var scale = GetScale();
            if (scale >= 2) return 2;
            if (scale >= 1.5) return 1.5f;
            return scale >= 1 ? 1 : 0.75f;
        }

        private static float GetDpi()
        {
            return 0f.Equals(Screen.dpi) ? 160 : Screen.dpi;
        }

        private static float GetScale()
        {
            return GetDpi()/160;
        }
    }
}