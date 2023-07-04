using SpaceShooter.gui;

namespace SpaceShooter.src.gui.game.grid
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

            GameOverLabel gameOverLbl = new GameOverLabel(this, "GAME OVER!");
            gameOverLbl.Top = Parent.ClientRectangle.Height / 2 - (int)(gameOverLabelHeightRatio * gameOverLbl.Height);

            CustomButton gameOverBtn = new CustomButton(this)
            {
                Width = (int)(Parent.Width * gameOverButtonWidthRatio),
                Height = (int)(Parent.Height * gameOverButtonHeightRatio),
                Top = gameOverLbl.Top + gameOverLbl.Height + 20,
                Text = "Continue"
            };
            gameOverBtn.Left = Parent.ClientRectangle.Width / 2 - gameOverBtn.Width / 2;
            gameOverBtn.Click += onGameOverButtonClick;
        }
        
        private void onGameOverButtonClick(Object? sender, EventArgs e) => gameForm.Close();
    }
}
