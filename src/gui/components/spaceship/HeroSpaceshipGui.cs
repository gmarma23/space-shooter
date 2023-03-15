namespace SpaceShooter.gui
{
    public class HeroSpaceshipGui : SpaceshipGui
    {
        public HeroSpaceshipGui(int width, int height, Image image) : base(width, height, image)
            => arrangeItems();

        public override void UpdateLocation(int newLocationX, int newLocationY)
            => Location = new Point(newLocationX, newLocationY);

        protected override void arrangeItems()
        {
            spaceshipPicBox.Top = 0;
            spaceshipHealthBar.Top = (int)((1 + healthBarMarginRatio) * spaceshipPicBox.Height);
        }
    }
}
