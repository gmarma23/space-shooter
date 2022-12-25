using SpaceShooter.core;

namespace SpaceShooter.gui
{
    internal class EnemySpaceshipGui : SpaceshipGui
    {
        private EnemySpaceshipPictureBox enemyPicBox;

        public EnemySpaceshipGui(EnemySpaceshipType type, int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) :
            base(spaceshipPicBoxWidth, spaceshipPicBoxHeight)
        {
            enemyPicBox = new EnemySpaceshipPictureBox(type, spaceshipPicBoxWidth, spaceshipPicBoxHeight);
            Controls.Add(enemyPicBox);
            arrangeItems();
        }

        public override void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY)
        {
            Location = new Point(newSpaceshipLocationX, newSpaceshipLocationY + enemyPicBox.Height - Height);
        }

        protected override void arrangeItems()
        {
            enemyPicBox.Top = (int)((healthBarHeightRatio + healthBarMarginRatio) * enemyPicBox.Height);
            spaceshipHealthBar.Top = 0;
        }
    }
}
