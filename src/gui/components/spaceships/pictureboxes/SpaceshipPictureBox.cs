namespace SpaceShooter.gui
{
    internal class SpaceshipPictureBox : PictureBox
    {
        public SpaceshipPictureBox(int width, int height) 
        {
            Width = width;
            Height = height;
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
        }
    }
}
