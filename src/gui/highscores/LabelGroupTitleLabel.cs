namespace SpaceShooter.gui
{
    public class LabelGroupTitleLabel : CustomLabel
    {
        private const float parentHeightRatio = 0.05f;
        private const float parentWidthRatio = 0.8f;

        public LabelGroupTitleLabel(Control parent, string text) 
            : base(parent, text, parentHeightRatio, parentWidthRatio, FontStyle.Bold)
        { }
    }
}
