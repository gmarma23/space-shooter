using SpaceShooter.core;

namespace SpaceShooter.gui
{
    internal abstract class SpaceshipGui : Panel
    {
        protected const double healthBarHeightRatio = 0.05;
        protected const double healthBarMarginRatio = 0.07;
        private const double explosionRatio = 0.75;

        protected SpaceshipPictureBox spaceshipPicBox;
        protected SpaceshipHealthBar spaceshipHealthBar;

        public SpaceshipGui(int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) 
        {
            Width = spaceshipPicBoxWidth;
            Height = spaceshipPicBoxHeight + (int)((healthBarHeightRatio + healthBarMarginRatio) * spaceshipPicBoxHeight);

            int healthBarWidth = spaceshipPicBoxWidth;
            int healthBarHeight = (int)(spaceshipPicBoxHeight * healthBarHeightRatio);

            spaceshipHealthBar = new SpaceshipHealthBar(healthBarWidth, healthBarHeight);
            Controls.Add(spaceshipHealthBar);
        }

        public abstract void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY);

        public void UpdateAvailableHealth(double availableHealthRatio) => spaceshipHealthBar.UpdateAvailableHealth(availableHealthRatio);

        public async void Explode(int duration = 1000)
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

        protected abstract void arrangeItems();
    }
}
