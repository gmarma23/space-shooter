using SpaceShooter.core;
using SpaceShooter.gui;
using System.Windows.Forms;

namespace SpaceShooter
{
    public static class GameManager
    {
        private static readonly Dictionary<string, KeyEventHandler> keyEventHandlers = new()
        {
            { "OnKeyDown", invokeHeroControls },
            { "OnKeyUp", freeHeroControls }
        };

        private static GameState gameState;
        private static GameFrame gameFrame;
        private static TimeManager timeManager;
        private static bool isEnemyBeingRenewed;

        public static void StartGame()
        {
            timeManager = new TimeManager();
            gameState = new GameState();
            gameFrame = new GameFrame(
                gameState.Grid.DimensionX,
                gameState.Grid.DimensionY,
                keyEventHandlers
            );
            isEnemyBeingRenewed = false;
            gameFrame.Show();

            gameFrame.RenderHeroSpaceship(gameState);

            gameState.RenewEnemySpaceship();
            gameFrame.RenderEnemySpaceship(gameState);

            timeManager.AddMainRecurringAction(gameLoop);
            timeManager.EnableTime();
        }

        private static void gameLoop(object? sender, EventArgs e)
        {
            if (gameState.IsGameOver())
            {
                gameOverActions();
                return;
            }

            renewEnemySpaceship();

            gameState.MoveGridItems();
            gameState.EnemyTeleport();
            gameState.DisposeInactiveWeapons();            

            gameFrame.RelocateSpaceship(gameState, true);
            gameFrame.RelocateSpaceship(gameState, false);
            gameFrame.UpdateSpaceshipAvailableHealth(gameState, true);
            gameFrame.UpdateSpaceshipAvailableHealth(gameState, false);

            gameState.SpaceshipFireLaser(false);
            gameState.EnemyLaunchMissile();

            gameFrame.UpdateActiveWeapons(gameState);
        }

        private static async void renewEnemySpaceship()
        {
            if (!gameState.IsEnemyDestroyed() || isEnemyBeingRenewed)
                return;
            
            isEnemyBeingRenewed = true;
            await gameFrame.DestroySpaceship(false);
            gameState.RenewEnemySpaceship();
            gameFrame.RenderEnemySpaceship(gameState);
            isEnemyBeingRenewed = false;
        }

        private static void invokeHeroControls(object? sender, KeyEventArgs e)
        {
            if (gameState.IsGameOver()) 
                return;

            if (e.KeyCode == Keys.Space)
                gameState.SpaceshipFireLaser(true);
            else
                toggleHeroMotionControls(e, true);
        }

        private static void freeHeroControls(object? sender, KeyEventArgs e)
        {
            if (gameState.IsGameOver()) 
                return;

            toggleHeroMotionControls(e, false);
        }

        private static void toggleHeroMotionControls(KeyEventArgs e, bool invoke)
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

        private static void gameOverActions()
        {
            gameFrame.DestroySpaceship(true);
            timeManager.DisableTime();
            MessageBox.Show("Game Over!");
            gameFrame.Close();
        }
    }
}
