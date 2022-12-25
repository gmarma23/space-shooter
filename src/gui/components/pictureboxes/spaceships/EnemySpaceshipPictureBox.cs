using SpaceShooter.core;

namespace SpaceShooter.gui
{
    internal class EnemySpaceshipPictureBox : SpaceshipPictureBox
    {
        private static readonly Dictionary<EnemySpaceshipType, Bitmap> enemyImages = new Dictionary<EnemySpaceshipType, Bitmap>()
        {
            {EnemySpaceshipType.Fighter, resources.Resources.enemy_fighter_spaceship },
            {EnemySpaceshipType.Teleporter, resources.Resources.enemy_teleporter_spaceship },
            {EnemySpaceshipType.Boss, resources.Resources.enemy_boss_spaceship }
        };

        public EnemySpaceshipPictureBox(EnemySpaceshipType type, int width, int height) : base(width, height)
        {
            Image = enemyImages[type];
        }
    }
}
