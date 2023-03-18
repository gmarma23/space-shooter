using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.gui
{
    public abstract class SpaceshipGui : Panel
    {
        protected const double healthBarHeightRatio = 0.05;
        protected const double healthBarMarginRatio = 0.07;
        private const double explosionRatio = 0.75;

        protected static readonly AudioStreamPlayer explosionSoundFx = new(Resources.aud_explosion);

        protected PictureBox spaceshipPicBox;
        protected SpaceshipHealthBar spaceshipHealthBar;

        public SpaceshipGui(int width, int height, Image image) 
        {
            Width = width;
            Height = height + (int)((healthBarHeightRatio + healthBarMarginRatio) * height);
            BackColor = Color.Transparent;

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

        public void UpdateAvailableHealth(float availableHealthRatio) 
            => spaceshipHealthBar.UpdateAvailableHealth(availableHealthRatio);

        public async Task Explode()
        {
            spaceshipHealthBar.Visible = false;

            PictureBox explosionPicBox = new PictureBox()
            {
                Width = (int)(spaceshipPicBox.Width * explosionRatio),
                Height = (int)(spaceshipPicBox.Height * explosionRatio),
                Image = Resources.img_explosion,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            Controls.Add(explosionPicBox);
            explosionPicBox.Parent = spaceshipPicBox;

            explosionSoundFx.Play();

            int steps = 10;
            for (int i = 0; i < steps; i++)
            {
                float opacity = (float)(steps - i) / (float)steps;
                spaceshipPicBox.Image = ImageUtils.SetOpacity((Bitmap)spaceshipPicBox.Image, opacity);
                await Task.Delay(100);
            }

            Image explosionImage = explosionPicBox.Image;
            explosionPicBox.Image = null;
            explosionImage.Dispose();
            Controls.Remove(explosionPicBox);
        }

        public void DisposePictureBox()
        {
            Image spaceshipPictureBoxImage = spaceshipPicBox.Image;
            spaceshipPicBox.Image = null;
            spaceshipPictureBoxImage.Dispose();
            Controls.Remove(spaceshipPicBox);
        }

        protected abstract void arrangeItems();
    }
}
