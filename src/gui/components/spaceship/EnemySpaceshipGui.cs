namespace SpaceShooter.gui
{
    internal class EnemySpaceshipGui : SpaceshipGui
    {
        public EnemySpaceshipGui(int width, int height, Image image) : base(width, height, image)
            => arrangeItems();

        public override void UpdateLocation(int newLocationX, int newLocationY)
            => Location = new Point(newLocationX, newLocationY + spaceshipPicBox.Height - Height);

        protected override void arrangeItems()
        {
            spaceshipPicBox.Top = (int)((healthBarHeightRatio + healthBarMarginRatio) * spaceshipPicBox.Height);
            spaceshipHealthBar.Top = 0;
        }
    }
}
