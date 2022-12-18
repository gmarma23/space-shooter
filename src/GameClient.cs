using SpaceShooter.core;
using SpaceShooter.gui;

using Timer = System.Timers.Timer;

namespace SpaceShooter
{
    internal static class GameClient
    {
        private static GameFrame gameFrame = new();
        private static Gameplay game = new();
        private static Timer relocateHeroTimer = new();
        private static Timer relocateEnemiesTimer = new();
        private static Timer relocateLaserBlastsTimer = new();
        private static Timer relocateEnemyMissileTimer = new();
        private static Timer bringEnemyToViewportTimer = new();
        private static Timer enemyFireLaserTimer = new();
        private static Timer enemyLaunchMissileTimer = new();
        private static Timer enemyTeleportTimer = new();

        public static void StartGame()
        {
            gameFrame = ;
            game = ;
            relocateHeroTimer = new Timer();
            relocateEnemiesTimer = new Timer();
            relocateLaserBlastsTimer = new Timer();
            relocateEnemyMissileTimer = new Timer();
            bringEnemyToViewportTimer = new Timer();
            enemyFireLaserTimer = new Timer();
            enemyLaunchMissileTimer = new Timer();
            enemyTeleportTimer = new Timer();
        }

        public static void OnKeyPress(Keys keys)
        {
 
        }

        public static void OnKeyRelease(Keys keys)
        {

        }

        public static void OnEnemyDestroy()
        {

        }
    }
}
