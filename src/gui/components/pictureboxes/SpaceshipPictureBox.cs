namespace SpaceShooter.gui
{
    internal class SpaceshipPictureBox : PictureBox
    {
        private static readonly Dictionary<SpaceshipType, Bitmap> spaceshipImages = new Dictionary<SpaceshipType, Bitmap>() 
        { 
            {SpaceshipType.Hero, resources.Resources.hero_spaceship },
            {SpaceshipType.EnemyFighter, resources.Resources.enemy_fighter_spaceship },
            {SpaceshipType.EnemyTeleporter, resources.Resources.enemy_teleporter_spaceship },
            {SpaceshipType.EnemyBoss, resources.Resources.enemy_boss_spaceship }
        };

        public SpaceshipPictureBox(SpaceshipType type, int width, int height) 
        {
            Width = width;
            Height = height;
            Image = spaceshipImages[type];
        }

        public enum SpaceshipType
        {
            Hero,
            EnemyFighter,
            EnemyTeleporter,
            EnemyBoss
        }
    }
}
