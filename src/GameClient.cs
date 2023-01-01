using SpaceShooter.core;
using SpaceShooter.gui;

using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    internal class GameClient
    {
        private const int relocateGridItemsTime = 20;
        private const int spaceshipTeleportTime = 7000;

        private readonly Dictionary<string, KeyEventHandler> keyEventHandlers;

        private GameState game;
        private GameFrame gameFrame;

        private Timer updateGridItemsTimer;
        private Timer bringEnemyToViewportTimer;
        private Timer enemyFireLaserTimer;
        private Timer enemyLaunchMissileTimer;
        private Timer teleportSpaceshipsTimer;
        private Timer heroLaserReloadTimer;

        public GameClient()
        {
            keyEventHandlers = new Dictionary<string, KeyEventHandler>
            {
                { "OnKeyDown", invokeHeroControls },
                { "OnKeyUp", freeHeroControls }
            };

            game = new GameState();
            gameFrame= new GameFrame(game.GridDimensionX, game.GridDimensionY, keyEventHandlers);

            updateGridItemsTimer = new Timer();
            bringEnemyToViewportTimer = new Timer();
            enemyFireLaserTimer = new Timer();
            enemyLaunchMissileTimer = new Timer();
            teleportSpaceshipsTimer = new Timer();
            heroLaserReloadTimer = new Timer();

            updateGridItemsTimer.Interval = relocateGridItemsTime;
            updateGridItemsTimer.Tick += onUpdateGridItemsTimerTick;

            teleportSpaceshipsTimer.Interval = spaceshipTeleportTime;
            teleportSpaceshipsTimer.Tick += onSpaceshipTeleportTimerTick;

            heroLaserReloadTimer.Interval = game.GetSpaceshipLaserReloadTime(true);
            heroLaserReloadTimer.Tick += onHeroLaserReloadTimerTick;

            gameFrame.Show();
        }

        public void StartGame()
        {
            gameFrame.RenderHeroSpaceship(game);
            gameFrame.RelocateSpaceship(game, true);

            game.RenewEnemySpaceship(EnemySpaceshipType.Teleporter);
            gameFrame.RenderEnemySpaceship(game);
            gameFrame.RelocateSpaceship(game, false);

            enemyFireLaserTimer.Interval = game.GetSpaceshipLaserReloadTime(false);
            enemyFireLaserTimer.Tick += onEnemyFireLaserTimerTick;
            enemyFireLaserTimer.Enabled = true;

            updateGridItemsTimer.Enabled = true;
            teleportSpaceshipsTimer.Enabled = true;
        }

        private void invokeHeroControls(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                fireAndRenderSpaceshipLaser(true);
                game.IsHeroLaserReloading(true);
                heroLaserReloadTimer.Enabled = true;
            }
            else
                toggleHeroMotionControls(e, true);
        }

        private void freeHeroControls(object sender, KeyEventArgs e) => toggleHeroMotionControls(e, false);

        private void toggleHeroMotionControls(KeyEventArgs e, bool invoke)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.HeroGoesLeft(invoke);
                    break;
                case Keys.Right:
                    game.HeroGoesRight(invoke);
                    break;
                case Keys.Up:
                    game.HeroGoesUp(invoke);
                    break;
                case Keys.Down:
                    game.HeroGoesDown(invoke);
                    break;
            };
        }

        private void fireAndRenderSpaceshipLaser(bool isHero)
        {
            List<int> firedLaserBlastsNumCodes = game.SpaceshipFireLaser(isHero);
            foreach (int numCode in firedLaserBlastsNumCodes)
                gameFrame.RenderLaserBlast(game, numCode);
            gameFrame.RelocateLaserBlasts(game);
        }

        private void moveSpaceships()
        {
            game.MoveSpaceship(true);
            gameFrame.RelocateSpaceship(game, true);

            game.MoveSpaceship(false);
            gameFrame.RelocateSpaceship(game, false);
        }

        private void updateSpaceshipsAvailableHealth()
        {
            gameFrame.UpdateSpaceshipAvailableHealth(game, true);
            gameFrame.UpdateSpaceshipAvailableHealth(game, false);
        }

        private void moveActiveLaserBlasts()
        {
            game.MoveLaserBlasts();
            gameFrame.RelocateLaserBlasts(game);
        }

        private void removeInactiveLaserBlasts()
        {
            List<int> disposedLaserBlastsNumCodes = game.DisposeInactiveLaserBlast();
            foreach (int numCode in disposedLaserBlastsNumCodes)
                gameFrame.DisposeInactiveLaserBlast(numCode);
        }

        private void onUpdateGridItemsTimerTick(object sender, EventArgs e)
        {
            moveSpaceships();
            moveActiveLaserBlasts();
            updateSpaceshipsAvailableHealth();
            removeInactiveLaserBlasts();

            if (game.IsGameOver())
            {
                gameFrame.SpaceshipExplode(true);
                updateGridItemsTimer.Enabled = false;
            }
                

        }

        private void onSpaceshipTeleportTimerTick(object sender, EventArgs e)
        {
            game.TeleportEnemySpaceship();
            gameFrame.RelocateSpaceship(game, false);
        }

        private void onHeroLaserReloadTimerTick(object sender, EventArgs e)
        {
            game.IsHeroLaserReloading(false);
            heroLaserReloadTimer.Enabled = false;
        }

        private void onEnemyFireLaserTimerTick(object sender, EventArgs e)
        {
            fireAndRenderSpaceshipLaser(false);
        }
    }
}
