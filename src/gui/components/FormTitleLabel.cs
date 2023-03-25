namespace SpaceShooter.gui
{
    public class FormTitleLabel : CustomLabel
    {
        private const float parentLocationYRatio = 0.05f;
        private const float parentHeightRatio = 0.22f;
        private const float parentWidthRatio = 0.7f;

        public FormTitleLabel(Control parent, string text) 
            : base(parent, text, parentHeightRatio, parentWidthRatio, FontStyle.Bold | FontStyle.Italic)
        {
            Top = (int)(Parent.ClientRectangle.Height * parentLocationYRatio);
        }
    }
}
