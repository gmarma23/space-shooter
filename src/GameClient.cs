using SpaceShooter.core;
using SpaceShooter.gui;

using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    internal class GameClient
    {
        private const int relocateGridItemsTime = 20;

        private readonly Dictionary<string, KeyEventHandler> keyEventHandlers;

        private GameState game;
        private GameFrame gameFrame;

        private Timer relocateGridItemsTimer;
        private Timer bringEnemyToViewportTimer;
        private Timer enemyFireLaserTimer;
        private Timer enemyLaunchMissileTimer;
        private Timer enemyTeleportTimer;
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

            relocateGridItemsTimer = new Timer();
            bringEnemyToViewportTimer = new Timer();
            enemyFireLaserTimer = new Timer();
            enemyLaunchMissileTimer = new Timer();
            enemyTeleportTimer = new Timer();
            heroLaserReloadTimer = new Timer();

            relocateGridItemsTimer.Interval = relocateGridItemsTime;
            relocateGridItemsTimer.Tick += onMoveGridItemsTimerTick;

            heroLaserReloadTimer.Interval = game.GetSpaceshipLaserReloadTime(true);
            heroLaserReloadTimer.Tick += onHeroLaserReloadTimerTick;

            

            gameFrame.Show();
        }

        public void StartGame()
        {
            gameFrame.RenderHeroSpaceship(game);
            gameFrame.RelocateSpaceship(game, true);

            game.RenewEnemySpaceship(EnemySpaceshipType.Fighter);
            gameFrame.RenderEnemySpaceship(game);
            gameFrame.RelocateSpaceship(game, false);

            enemyFireLaserTimer.Interval = game.GetSpaceshipLaserReloadTime(false);
            enemyFireLaserTimer.Tick += onEnemyFireLaserTimerTick;
            enemyFireLaserTimer.Enabled = true;

            relocateGridItemsTimer.Enabled = true;
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

        private void onMoveGridItemsTimerTick(object sender, EventArgs e)
        {
            moveSpaceships();
            moveActiveLaserBlasts();
            removeInactiveLaserBlasts();
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
