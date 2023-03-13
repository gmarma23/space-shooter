using SpaceShooter.core;
using SpaceShooter.gui;
using SpaceShooter.src.core.grid;
using System.Diagnostics;
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

        private static GameState? gameState = null;
        private static GameFrame? gameFrame = null;
        private static TimeManager? timeManager = null;
        private static bool isEnemyBeingRenewed;

        public static void StartNewGame()
        {
            timeManager = new TimeManager();
            gameState = new GameState();
            gameFrame = new GameFrame(
                gameState.Grid.DimensionX,
                gameState.Grid.DimensionY,
                keyEventHandlers,
                onGameFrameClosed
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
            Debug.Assert(gameState != null);
            Debug.Assert(gameFrame != null);
            Debug.Assert(timeManager != null);

            timeManager.UpdateDeltaTime();

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
            Debug.Assert(gameState != null);
            Debug.Assert(gameFrame != null);

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
            Debug.Assert(gameState != null);

            if (gameState.IsGameOver()) 
                return;

            if (e.KeyCode == Keys.Space)
            {
                gameState.SpaceshipFireLaser(true);
                return;
            }
               
            toggleHeroMotionControls(e, true);
        }

        private static void freeHeroControls(object? sender, KeyEventArgs e)
        {
            Debug.Assert(gameState != null);

            if (gameState.IsGameOver()) 
                return;

            toggleHeroMotionControls(e, false);
        }

        private static void toggleHeroMotionControls(KeyEventArgs e, bool isInvoked)
        {
            Debug.Assert(gameState != null);

            IControls conrolableHero = gameState.GetControlableHero();
            switch (e.KeyCode)
            {
                case Keys.Left:
                    conrolableHero.GoLeft = isInvoked;
                    break;
                case Keys.Right:
                    conrolableHero.GoRight = isInvoked;
                    break;
                case Keys.Up:
                    conrolableHero.GoUp = isInvoked;
                    break;
                case Keys.Down:
                    conrolableHero.GoDown = isInvoked;
                    break;
                default:
                    break;
            };
        }

        private static void gameOverActions()
        {
            Debug.Assert(gameFrame != null);

            gameFrame.DestroySpaceship(true);
            timeManager.DisableTime();
            MessageBox.Show("Game Over!");
            gameFrame.Close();
        }

        private static void onGameFrameClosed(object? sender, EventArgs e)
        {
            Debug.Assert(timeManager != null);

            timeManager.DisableTime();
            gameState = null;
            gameFrame = null;
            timeManager = null;
        }
    }
}
