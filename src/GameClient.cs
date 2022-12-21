using SpaceShooter.core;
using SpaceShooter.gui;

using Timer = System.Timers.Timer;

namespace SpaceShooter
{
    internal class GameClient
    {
        private GameFrame gameFrame;
        private Gameplay game;
        private Timer relocateHeroTimer;
        private Timer relocateEnemiesTimer;
        private Timer relocateLaserBlastsTimer;
        private Timer relocateEnemyMissileTimer;
        private Timer bringEnemyToViewportTimer;
        private Timer enemyFireLaserTimer;
        private Timer enemyLaunchMissileTimer;
        private Timer enemyTeleportTimer;

        public GameClient()
        {
            gameFrame= new GameFrame(800, 600);
            gameFrame.Show();

        }

        public void StartGame()
        {
            
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
