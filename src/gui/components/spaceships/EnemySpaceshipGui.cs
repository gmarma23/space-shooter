using SpaceShooter.core;

namespace SpaceShooter.gui
{
    internal class EnemySpaceshipGui : SpaceshipGui
    {
        public EnemySpaceshipGui(EnemySpaceshipType type, int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) :
            base(spaceshipPicBoxWidth, spaceshipPicBoxHeight)
        {
            spaceshipPicBox = new EnemySpaceshipPictureBox(type, spaceshipPicBoxWidth, spaceshipPicBoxHeight);
            Controls.Add(spaceshipPicBox);
            arrangeItems();
        }

        public override void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY)
        {
            Location = new Point(newSpaceshipLocationX, newSpaceshipLocationY + spaceshipPicBox.Height - Height);
        }

        protected override void arrangeItems()
        {
            spaceshipPicBox.Top = (int)((healthBarHeightRatio + healthBarMarginRatio) * spaceshipPicBox.Height);
            spaceshipHealthBar.Top = 0;
        }
    }
}
