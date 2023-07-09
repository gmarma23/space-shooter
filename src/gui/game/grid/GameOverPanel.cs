namespace SpaceShooter.gui
{
    public class GameOverPanel : Panel
    {
        private const float gameOverLabelHeightRatio = 1.5f;
        private const float gameOverButtonWidthRatio = 0.1f;
        private const float gameOverButtonHeightRatio = 0.05f;

        private GameForm gameForm;

        public GameOverPanel(Control parent, GameForm gameForm)
        {
            Parent = parent;
            this.gameForm = gameForm;

            Width = Parent.Width;
            Height = Parent.Height;
            BackColor = Color.Transparent;

            GameMessageLabel gameOverLbl = new GameMessageLabel(this, "GAME OVER!")
            {
                ForeColor = Color.DarkRed
            };
            gameOverLbl.Top = Parent.ClientRectangle.Height / 2 - (int)(gameOverLabelHeightRatio * gameOverLbl.Height);

            CustomButton gameOverBtn = new CustomButton(this, "Continue", gameOverButtonWidthRatio, gameOverButtonHeightRatio)
            {
                Top = gameOverLbl.Top + gameOverLbl.Height + 20,
            };
            gameOverBtn.Left = Parent.ClientRectangle.Width / 2 - gameOverBtn.Width / 2;
            gameOverBtn.Click += onGameOverButtonClick;
        }
        
        private void onGameOverButtonClick(object? sender, EventArgs e) => gameForm.Close();
    }
}
