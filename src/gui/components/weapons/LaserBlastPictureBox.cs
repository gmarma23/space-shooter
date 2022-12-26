namespace SpaceShooter.gui
{
    internal class LaserBlastPictureBox : PictureBox
    {
        public int NumCode { get; private init; }

        public LaserBlastPictureBox(bool isHero, int numCode, int width, int height)
        {
            NumCode = numCode;
            Width = width;
            Height = height;
            BackColor = Color.Transparent;
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = isHero ? resources.Resources.blue_laser_blast : resources.Resources.red_laser_blast;
        }

        public void UpdateLocation(int newX, int newY)
        {
            Location = new Point(newX, newY);
        }
    }
}
