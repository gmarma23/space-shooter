using SpaceShooter.core;
using SpaceShooter.gui;

using Timer = System.Timers.Timer;

namespace SpaceShooter
{
    internal class GameClient
    {
        private GameState game;
        private GameFrame gameFrame;

        private Timer relocateGridItemsTimer;
        private Timer bringEnemyToViewportTimer;
        private Timer enemyFireLaserTimer;
        private Timer enemyLaunchMissileTimer;
        private Timer enemyTeleportTimer;

        public GameClient()
        {
            game = new GameState();
            gameFrame= new GameFrame(game.GridDimensionX, game.GridDimensionY);
            gameFrame.Show();

        }

        public void StartGame()
        {
            gameFrame.RenderHeroSpaceship(game);
            gameFrame.RelocateHeroSpaceship(game);
        }

        public void OnKeyPress(Keys keys)
        {
 
        }

        public void OnKeyRelease(Keys keys)
        {

        }

        public void OnEnemyDestroy()
        {

        }
    }
}
