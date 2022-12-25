namespace SpaceShooter.gui
{
    internal class HeroSpaceshipGui : SpaceshipGui
    {
        private HeroSpaceshipPictureBox heroPicBox;

        public HeroSpaceshipGui(int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) : 
            base(spaceshipPicBoxWidth, spaceshipPicBoxHeight)
        {
            heroPicBox = new HeroSpaceshipPictureBox(spaceshipPicBoxWidth, spaceshipPicBoxHeight);
            Controls.Add(heroPicBox);
            arrangeItems();
        }

        public override void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY)
        {
            Location = new Point(newSpaceshipLocationX, newSpaceshipLocationY);
        }

        protected override void arrangeItems()
        {
            heroPicBox.Top = 0;
            spaceshipHealthBar.Top = (int)((1 + healthBarMarginRatio) * heroPicBox.Height);
        }
    }
}
