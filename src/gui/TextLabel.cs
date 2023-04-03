namespace SpaceShooter.gui
{
    public class TextLabel : CustomLabel
    {
        private const float parentHeightRatio = 0.3f;
        private const float parentWidthRatio = 0.8f;

        public TextLabel(Control parent, string text)
            : base(parent, text, parentHeightRatio, parentWidthRatio)
        {
            TextAlign = ContentAlignment.MiddleLeft;
        }
    }
}
