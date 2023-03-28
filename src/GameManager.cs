using SpaceShooter.core;
using SpaceShooter.gui;
using SpaceShooter.src.core.grid;
using SpaceShooter.utils;
using System.Diagnostics;

namespace SpaceShooter
{
    public static class GameManager
    {
        private static GameState? gameState = null;
        private static GameForm? gameForm = null;
        private static TimeManager? timeManager = null;
        private static bool isEnemyBeingRenewed;

        public static void StartNewGame()
        {
            timeManager = new TimeManager(65);
            gameState = new GameState(1360, 760);
            gameForm = new GameForm(gameState);
            isEnemyBeingRenewed = false;

            gameForm.Deactivate += gameFormLostFocusActions;
            gameForm.FormClosed += gameFormClosedActions;
            gameForm.KeyDown += invokeHeroControls;
            gameForm.KeyUp += freeHeroControls;

            gameForm.Show();

            gameState.RenewEnemySpaceship();

            gameForm.Grid.RenderHeroSpaceship(gameState);
            gameForm.Grid.RenderEnemySpaceship(gameState);
            gameForm.StatsBar.ScoreLabel.UpdateValue(gameState.Score.ToString());

            timeManager.AddMainRecurringAction(gameLoop);
            timeManager.EnableTime();
        }

        private static void gameLoop(object? sender, EventArgs e)
        {
            Debug.Assert(gameState != null);
            Debug.Assert(gameForm != null);
            Debug.Assert(timeManager != null);

            timeManager.UpdateDeltaTime();
            string elapsedTime = StringUtils.FormatSecondsToHMS(TimeManager.ElapsedGameTime);
            gameForm.StatsBar.ElapsedTimeLabel.UpdateValue(elapsedTime);

            if (gameState.IsGameOver())
            {
                gameOverActions();
                return;
            }

            renewEnemySpaceship();

            gameState.MoveGridItems();
            gameState.EnemyTeleport();
            gameState.DisposeInactiveCollidableItems();            

            gameForm.Grid.RelocateSpaceship(gameState, true);
            gameForm.Grid.RelocateSpaceship(gameState, false);
            gameForm.Grid.UpdateSpaceshipAvailableHealth(gameState, true);
            gameForm.Grid.UpdateSpaceshipAvailableHealth(gameState, false);

            gameState.SpaceshipFireLaser(false);
            gameState.EnemyLaunchMissile();

            gameForm.Grid.UpdateActiveCollidableItems(gameState);
        }

        private static async void renewEnemySpaceship()
        {
            Debug.Assert(gameState != null);
            Debug.Assert(gameForm != null);

            if (!gameState.IsEnemyDestroyed() || isEnemyBeingRenewed)
                return;
            
            isEnemyBeingRenewed = true;
            await gameForm.Grid.DestroySpaceship(false);
            gameState.RenewEnemySpaceship();
            gameForm.Grid.RenderEnemySpaceship(gameState);
            gameForm.StatsBar.ScoreLabel.UpdateValue(gameState.Score.ToString());
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
                case Keys.A:
                case Keys.Left:
                    conrolableHero.GoLeft = isInvoked;
                    break;
                case Keys.D:
                case Keys.Right:
                    conrolableHero.GoRight = isInvoked;
                    break;
                case Keys.W:
                case Keys.Up:
                    conrolableHero.GoUp = isInvoked;
                    break;
                case Keys.S:
                case Keys.Down:
                    conrolableHero.GoDown = isInvoked;
                    break;
                default:
                    break;
            };
        }

        private static async void gameOverActions()
        {
            Debug.Assert(gameState != null);
            Debug.Assert(gameForm != null);
            Debug.Assert(timeManager != null);

            timeManager.DisableTime();
            await gameForm.Grid.DestroySpaceship(true);
            gameForm.Grid.GameOverActions();

            string gameDuration = StringUtils.FormatSecondsToHMS(TimeManager.ElapsedGameTime);
            DatabaseManager.AddEntry(gameState.Score, gameDuration);
        }

        private static void gameFormLostFocusActions(object? sender, EventArgs e)
        {
            Debug.Assert(gameState != null);

            IControls conrolableHero = gameState.GetControlableHero();
            conrolableHero.ResetControls();
        }

        private static void gameFormClosedActions(object? sender, EventArgs e)
        {
            Debug.Assert(timeManager != null);
            Debug.Assert(gameForm != null);

            timeManager.DisableTime();
            gameForm.Grid.DisposeBackgroundImage();

            gameState = null;
            gameForm = null;
            timeManager = null;
        }
    }
}
