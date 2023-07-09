namespace SpaceShooter.gui
{
    internal class PictureBoxOptionsGroup : OptionsGroup
    {
        private List<PictureBox> options;

        public PictureBoxOptionsGroup(Control parent, string title, List<(Bitmap, Size)> optionDetails) : base(parent, title)
        {
            options = new List<PictureBox>();
            addOptions(optionDetails);
        }

        public void DisposePictureBoxes()
        {
            foreach (var pictureBox in options)
            {
                Image? image = pictureBox.Image;

                if (image == null)
                    continue;

                pictureBox.Image = null;
                image.Dispose();
                Controls.Remove(pictureBox);
            }
        }

        private void addOptions(List<(Bitmap, Size)> optionDetails)
        {
            foreach ((Bitmap image, Size imageSize) in optionDetails)
            {
                PictureBox newOption = new PictureBox()
                {
                    Parent = this,
                    
                    Size = imageSize,
                    Image = image,
                    SizeMode = PictureBoxSizeMode.StretchImage,

                    Top = (int)(Height * topMarginRatio),
                    Left = (options.Count == 0 ? 0 : options.Last().Left + options.Last().Width) + (int)(Width * leftMarginRatio),
                };
                Controls.Add(newOption);

                options.Add(newOption);
            }
        }
    }
}
