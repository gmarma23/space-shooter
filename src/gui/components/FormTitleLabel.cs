namespace SpaceShooter.gui
{
    public class FormTitleLabel : Label
    {
        private const float titleLocationYRatio = 0.02f;
        private const float titleWidthRatio = 0.8f;
        private const float titleHeightRatio = 0.3f;

        public FormTitleLabel(Control parent) 
        {
            Parent = parent;
            Parent.Controls.Add(this);
            BackColor = Color.Transparent;
            Width = (int)(titleWidthRatio * Parent.ClientRectangle.Width);
            Height = (int)(titleHeightRatio * Parent.ClientRectangle.Height);
            Font = new Font(
                "Microsoft Sans Serif",
                25.8F, FontStyle.Bold | FontStyle.Italic,
                GraphicsUnit.Point
            );
            ForeColor = Color.White;
            TextAlign = ContentAlignment.MiddleCenter;
            UseCompatibleTextRendering = true;
            Location = new Point(
                Parent.ClientRectangle.Width / 2 - Width / 2,
                (int)(Parent.ClientRectangle.Height * titleLocationYRatio)
            );
        }
    }
}
