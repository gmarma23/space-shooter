using SpaceShooter.core;
using SpaceShooter.gui;

namespace SpaceShooter
{
    internal class GameManager
    {
        private readonly GameState gameState;
        private readonly GameFrame gameFrame;
        private readonly TimeManager timeManager;
        private readonly Dictionary<string, KeyEventHandler> keyEventHandlers;

        public GameManager()
        {
            keyEventHandlers = new Dictionary<string, KeyEventHandler>
            {
                { "OnKeyDown", invokeHeroControls },
                { "OnKeyUp", freeHeroControls }
            };

            gameState = new GameState();
            gameFrame= new GameFrame(gameState.Grid.DimensionX, gameState.Grid.DimensionY, keyEventHandlers);
            timeManager = new TimeManager();

            gameFrame.Show();
        }

        public void StartGame()
        {
            gameFrame.RenderHeroSpaceship(gameState);
            gameFrame.RelocateSpaceship(gameState, true);

            gameState.RenewEnemySpaceship();
            gameFrame.RenderEnemySpaceship(gameState);
            gameFrame.RelocateSpaceship(gameState, false);

            timeManager.AddMainRecurringAction(gameLoop);
            timeManager.EnableTime();
        }

        private void gameLoop(object? sender, EventArgs e)
        {
            if (gameState.IsGameOver())
            {
                gameOverActions();
                return;
            }

            if (gameState.IsEnemyDestroyed())
                gameState.RenewEnemySpaceship();

            gameState.MoveGridItems();
            gameState.DisposeInactiveWeapons();
            gameState.EnemyTeleport();

            gameFrame.RelocateSpaceship(gameState, true);
            gameFrame.RelocateSpaceship(gameState, false);
            gameFrame.UpdateSpaceshipAvailableHealth(gameState, true);
            gameFrame.UpdateSpaceshipAvailableHealth(gameState, false);

            gameState.SpaceshipFireLaser(false);
            gameState.EnemyLaunchMissile();

            gameFrame.UpdateActiveWeapons(gameState);
        }

        private void invokeHeroControls(object? sender, KeyEventArgs e)
        {
            if (gameState.IsGameOver()) 
                return;

            if (e.KeyCode == Keys.Space)
                gameState.SpaceshipFireLaser(true);
            else
                toggleHeroMotionControls(e, true);
        }

        private void freeHeroControls(object? sender, KeyEventArgs e)
        {
            if (gameState.IsGameOver()) 
                return;

            toggleHeroMotionControls(e, false);
        }

        private void toggleHeroMotionControls(KeyEventArgs e, bool invoke)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    gameState.GetControlableHero().GoLeft = invoke;
                    break;
                case Keys.Right:
                    gameState.GetControlableHero().GoRight = invoke;
                    break;
                case Keys.Up:
                    gameState.GetControlableHero().GoUp = invoke;
                    break;
                case Keys.Down:
                    gameState.GetControlableHero().GoDown = invoke;
                    break;
            };
        }

        private void gameOverActions()
        {
            gameFrame.SpaceshipExplode(true);
            timeManager.DisableTime();
            MessageBox.Show("Game Over!");
            gameFrame.Close();
        }
    }
}
