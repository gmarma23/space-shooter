namespace SpaceShooter.gui
{
    public class HighscoreLabel : Label
    {
        public HighscoreLabel(Control parent)
        {
            Parent = parent;
            Parent.Controls.Add(this);
            BackColor = Color.Transparent;
            AutoSize = true;
            Font = new Font(
                "Microsoft Sans Serif",
                16.0F, GraphicsUnit.Point
            );
            ForeColor = Color.White;
            TextAlign = ContentAlignment.MiddleCenter;
            UseCompatibleTextRendering = true;
            Left = Parent.ClientRectangle.Width / 2 - Width / 2;
        }
    }
}
