namespace SpaceShooter.gui
{
    public class GameOverLabel : CustomLabel
    {
        private const float parentHeightRatio = 0.15f;
        private const float parentWidthRatio = 0.5f;

        public GameOverLabel(Control parent, string text)
            : base(parent, text, parentHeightRatio, parentWidthRatio, FontStyle.Bold | FontStyle.Italic)
        {
            ForeColor = Color.DarkRed;
        }
    }
}
