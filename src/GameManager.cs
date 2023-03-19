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
        private static GameFrame? gameFrame = null;
        private static TimeManager? timeManager = null;
        private static bool isEnemyBeingRenewed;

        public static void StartNewGame()
        {
            timeManager = new TimeManager(65);
            gameState = new GameState(1360, 760);
            gameFrame = new GameFrame(gameState);
            isEnemyBeingRenewed = false;

            gameFrame.Deactivate += gameFrameLostFocusActions;
            gameFrame.FormClosed += gameFrameClosedActions;
            gameFrame.KeyDown += invokeHeroControls;
            gameFrame.KeyUp += freeHeroControls;

            gameFrame.Show();

            gameState.RenewEnemySpaceship();

            gameFrame.Grid.RenderHeroSpaceship(gameState);
            gameFrame.Grid.RenderEnemySpaceship(gameState);
            gameFrame.StatsBar.ScoreLabel.UpdateValue(gameState.Score.ToString());

            timeManager.AddMainRecurringAction(gameLoop);
            timeManager.EnableTime();
        }

        private static void gameLoop(object? sender, EventArgs e)
        {
            Debug.Assert(gameState != null);
            Debug.Assert(gameFrame != null);
            Debug.Assert(timeManager != null);

            timeManager.UpdateDeltaTime();
            string elapsedTime = StringUtils.FormatSecondsToHMS(TimeManager.ElapsedGameTime);
            gameFrame.StatsBar.ElapsedTimeLabel.UpdateValue(elapsedTime);

            if (gameState.IsGameOver())
            {
                gameOverActions();
                return;
            }

            renewEnemySpaceship();

            gameState.MoveGridItems();
            gameState.EnemyTeleport();
            gameState.DisposeInactiveWeapons();            

            gameFrame.Grid.RelocateSpaceship(gameState, true);
            gameFrame.Grid.RelocateSpaceship(gameState, false);
            gameFrame.Grid.UpdateSpaceshipAvailableHealth(gameState, true);
            gameFrame.Grid.UpdateSpaceshipAvailableHealth(gameState, false);

            gameState.SpaceshipFireLaser(false);
            gameState.EnemyLaunchMissile();

            gameFrame.Grid.UpdateActiveWeapons(gameState);
        }

        private static async void renewEnemySpaceship()
        {
            Debug.Assert(gameState != null);
            Debug.Assert(gameFrame != null);

            if (!gameState.IsEnemyDestroyed() || isEnemyBeingRenewed)
                return;
            
            isEnemyBeingRenewed = true;
            await gameFrame.Grid.DestroySpaceship(false);
            gameState.RenewEnemySpaceship();
            gameFrame.Grid.RenderEnemySpaceship(gameState);
            gameFrame.StatsBar.ScoreLabel.UpdateValue(gameState.Score.ToString());
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

        private static async void gameOverActions()
        {
            Debug.Assert(gameState != null);
            Debug.Assert(gameFrame != null);
            Debug.Assert(timeManager != null);

            timeManager.DisableTime();
            await gameFrame.Grid.DestroySpaceship(true);
            gameFrame.Grid.GameOverActions();

            string gameDuration = StringUtils.FormatSecondsToHMS(TimeManager.ElapsedGameTime);
            DatabaseManager.AddEntry(gameState.Score, gameDuration);
        }

        private static void gameFrameLostFocusActions(object? sender, EventArgs e)
        {
            Debug.Assert(gameState != null);

            IControls conrolableHero = gameState.GetControlableHero();
            conrolableHero.ResetControls();
        }

        private static void gameFrameClosedActions(object? sender, EventArgs e)
        {
            Debug.Assert(timeManager != null);
            Debug.Assert(gameFrame != null);

            timeManager.DisableTime();
            gameFrame.Grid.DisposeBackgroundImage();

            gameState = null;
            gameFrame = null;
            timeManager = null;
        }
    }
}
