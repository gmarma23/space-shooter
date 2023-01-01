namespace SpaceShooter.gui
{
    internal class HeroSpaceshipGui : SpaceshipGui
    {
        public HeroSpaceshipGui(int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) : 
            base(spaceshipPicBoxWidth, spaceshipPicBoxHeight)
        {
            spaceshipPicBox = new HeroSpaceshipPictureBox(spaceshipPicBoxWidth, spaceshipPicBoxHeight);
            Controls.Add(spaceshipPicBox);
            arrangeItems();
        }

        public override void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY)
        {
            Location = new Point(newSpaceshipLocationX, newSpaceshipLocationY);
        }

        protected override void arrangeItems()
        {
            spaceshipPicBox.Top = 0;
            spaceshipHealthBar.Top = (int)((1 + healthBarMarginRatio) * spaceshipPicBox.Height);
        }
    }
}
