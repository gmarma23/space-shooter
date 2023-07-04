using SpaceShooter.gui;

namespace SpaceShooter.src.gui.game.grid
{
    public class GamePausedPanel : Panel
    {
        public GamePausedPanel(Control parent)
        {
            Parent = parent;

            Width = Parent.Width;
            Height = Parent.Height;
            BackColor = Color.FromArgb(255, 40, 40, 40);

            GameMessageLabel gamePausedLbl = new GameMessageLabel(this, "PAUSE")
            {
                ForeColor = Color.White
            };
            gamePausedLbl.Top = Parent.ClientRectangle.Height / 2 - gamePausedLbl.Height / 2;

            BringToFront();
            Hide();
        }
    }
}
