using SpaceShooter.gui;

namespace SpaceShooter.src.gui.game.grid
{
    public class GamePausedPanel : Panel
    {
        private const float gameOverLabelHeightRatio = 1.5f;
        private const float gameOverButtonWidthRatio = 0.1f;
        private const float gameOverButtonHeightRatio = 0.05f;

        private GameForm gameForm;

        public GamePausedPanel(Control parent, GameForm gameForm, EventHandler onGameResume)
        {
            Parent = parent;
            this.gameForm = gameForm;

            Width = Parent.Width;
            Height = Parent.Height;
            BackColor = Color.FromArgb(255, 40, 40, 40);

            GameMessageLabel gamePausedLbl = new GameMessageLabel(this, "PAUSE")
            {
                ForeColor = Color.White
            };
            gamePausedLbl.Top = Parent.ClientRectangle.Height / 2 - (int)(gameOverLabelHeightRatio * gamePausedLbl.Height);

            CustomButton resumeBtn = new CustomButton(this)
            {
                Width = (int)(Parent.Width * gameOverButtonWidthRatio),
                Height = (int)(Parent.Height * gameOverButtonHeightRatio),
                Top = gamePausedLbl.Top + gamePausedLbl.Height + 20,
                Text = "Resume"
            };
            resumeBtn.Left = Parent.ClientRectangle.Width / 2 - resumeBtn.Width / 2;
            resumeBtn.Click += onGameResume;

            CustomButton quitBtn = new CustomButton(this)
            {
                Width = (int)(Parent.Width * gameOverButtonWidthRatio),
                Height = (int)(Parent.Height * gameOverButtonHeightRatio),
                Top = resumeBtn.Top + resumeBtn.Height + 20,
                Text = "Quit"
            };
            quitBtn.Left = Parent.ClientRectangle.Width / 2 - quitBtn.Width / 2;
            quitBtn.Click += onQuitButtonClick;

            BringToFront();
            Hide();
        }

        private void onQuitButtonClick(object? sender, EventArgs e) => gameForm.Close();
    }
}
