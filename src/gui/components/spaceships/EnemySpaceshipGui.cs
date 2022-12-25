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

        protected override void arrangeItems()
        {
            enemyPicBox.Top = (int)((healthBarHeightRatio + healthBarMarginRatio) * enemyPicBox.Height);
            spaceshipHealthBar.Top = 0;
        }
    }
}
