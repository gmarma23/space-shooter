namespace SpaceShooter.gui
{
    public class GameMessageLabel : CustomLabel
    {
        private const float parentHeightRatio = 0.15f;
        private const float parentWidthRatio = 0.5f;

        public GameMessageLabel(Control parent, string text)
            : base(parent, text, parentHeightRatio, parentWidthRatio, FontStyle.Bold | FontStyle.Italic)
        { }
    }
}
