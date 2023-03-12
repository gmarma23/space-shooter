namespace SpaceShooter.gui
{
    internal abstract class SpaceshipGui : Panel
    {
        protected const double healthBarHeightRatio = 0.05;
        protected const double healthBarMarginRatio = 0.07;
        private const double explosionRatio = 0.75;

        protected PictureBox spaceshipPicBox;
        protected SpaceshipHealthBar spaceshipHealthBar;

        public SpaceshipGui(int width, int height, Image image) 
        {
            Width = width;
            Height = height + (int)((healthBarHeightRatio + healthBarMarginRatio) * height);

            spaceshipPicBox = new PictureBox
            {
                Width = width,
                Height = height,
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            Controls.Add(spaceshipPicBox);

            int healthBarWidth = Width;
            int healthBarHeight = (int)(Height * healthBarHeightRatio);

            spaceshipHealthBar = new SpaceshipHealthBar(healthBarWidth, healthBarHeight);
            Controls.Add(spaceshipHealthBar);
        }

        public abstract void UpdateLocation(int newLocationX, int newLocationY);

        protected abstract void arrangeItems();

        public void UpdateAvailableHealth(float availableHealthRatio) 
            => spaceshipHealthBar.UpdateAvailableHealth(availableHealthRatio);

        public async Task Explode(int duration = 1000)
        {
            spaceshipHealthBar.Visible = false;

            PictureBox explosionPicBox = new PictureBox()
            {
                Width = (int)(spaceshipPicBox.Width * explosionRatio),
                Height = (int)(spaceshipPicBox.Height * explosionRatio),
                Image = resources.Resources.explosion,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            Controls.Add(explosionPicBox);
            explosionPicBox.Parent = spaceshipPicBox;

            await Task.Delay(duration);

            Image explosionImage = explosionPicBox.Image;
            explosionPicBox.Image = null;
            explosionImage.Dispose();
            explosionPicBox.Dispose();
        }
    }
}
