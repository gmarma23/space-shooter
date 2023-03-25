namespace SpaceShooter.gui
{
    public class HighscoreLabel : CustomLabel
    {
        private const float parentWidthRatio = 0.7f;

        public HighscoreLabel(Control parent, string text, float parentHeightRatio) 
            : base(parent, text, parentHeightRatio, parentWidthRatio)
        { }
    }
}
