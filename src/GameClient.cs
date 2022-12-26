using SpaceShooter.core;
using SpaceShooter.gui;
using System.Reflection.Metadata.Ecma335;
using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    internal class GameClient
    {
        private readonly Dictionary<string, KeyEventHandler> keyEventHandlers;

        private GameState game;
        private GameFrame gameFrame;

        private Timer relocateGridItemsTimer;
        private Timer bringEnemyToViewportTimer;
        private Timer enemyFireLaserTimer;
        private Timer enemyLaunchMissileTimer;
        private Timer enemyTeleportTimer;

        public GameClient()
        {
            keyEventHandlers = new Dictionary<string, KeyEventHandler>
            {
                { "OnKeyDown", InvokeHeroControls},
                { "OnKeyUp", FreeHeroControls}
            };

            game = new GameState();
            gameFrame= new GameFrame(game.GridDimensionX, game.GridDimensionY, keyEventHandlers);

            relocateGridItemsTimer = new Timer();
            bringEnemyToViewportTimer = new Timer();
            enemyFireLaserTimer = new Timer();
            enemyLaunchMissileTimer = new Timer();
            enemyTeleportTimer = new Timer();

            relocateGridItemsTimer.Interval = 20;
            relocateGridItemsTimer.Tick += onRelocateGridItemsTimerTick;

            gameFrame.Show();
        }

        public void StartGame()
        {
            gameFrame.RenderHeroSpaceship(game);
            gameFrame.RelocateSpaceship(game, true);

            relocateGridItemsTimer.Enabled = true;
        }

        private void InvokeHeroControls(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                List<int> firedLaserBlastsNumCodes = game.SpaceshipFireLaser(true);
                foreach (int numCode in firedLaserBlastsNumCodes)
                    gameFrame.RenderLaserBlast(game, numCode);
                gameFrame.RelocateLaserBlasts(game);
            }
            else
                toggleHeroMotionControls(e, true);
        }

        private void FreeHeroControls(object sender, KeyEventArgs e) => toggleHeroMotionControls(e, false);

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

        private void onRelocateGridItemsTimerTick(object sender, EventArgs e)
        {
            game.MoveSpaceship(true);
            gameFrame.RelocateSpaceship(game, true);

            game.MoveLaserBlasts();
            gameFrame.RelocateLaserBlasts(game);
        }
    }
}
