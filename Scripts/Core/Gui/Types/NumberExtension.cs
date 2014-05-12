namespace Assets.Scripts.Core.Gui.Types
{
    public static class NumberExtension
    {
        public static Number Pixel(this float value)
        {
            return Number.Pixel(value);
        }

        public static Number Percentage(this float value)
        {
            return Number.Percentage(value);
        }
    }
}