namespace SpaceShooter.gui
{
    public class WeaponGui : PictureBox
    {
        public int ID { get; private init; }

        public WeaponGui(int id, int width, int height, Image image)
        {
            ID = id;
            Width = width;
            Height = height;
            Image = image;
            BackColor = Color.Transparent;
            SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public void UpdateLocation(int newLocationX, int newLocationY)
            => Location = new Point(newLocationX, newLocationY);

        public void DisposeImage()
        {
            Image weaponImage = Image;
            Image = null;
            weaponImage.Dispose();
        }
    }
}
