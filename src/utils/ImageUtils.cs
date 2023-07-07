using System.Drawing.Imaging;

namespace SpaceShooter.utils
{
    public static class ImageUtils
    {
        public static Bitmap SetOpacity(Bitmap inputBitmap, float opacity)
        {
            float o = (opacity < 0 || opacity > 1) ? 1 : opacity;

            Bitmap outputBitmap = new Bitmap(inputBitmap.Width, inputBitmap.Height);
            Rectangle imageArea = new Rectangle(0, 0, inputBitmap.Width, inputBitmap.Height);

            float[][] matrixItems = 
            {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, o, 0},
                new float[] {0, 0, 0, 0, 1}
            };

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            using (Graphics graphics = Graphics.FromImage(outputBitmap))
                graphics.DrawImage(
                    inputBitmap, 
                    imageArea, 
                    imageArea.X, 
                    imageArea.Y, 
                    imageArea.Width, 
                    imageArea.Height, 
                    GraphicsUnit.Pixel, 
                    imageAttributes
                );

            return outputBitmap;
        }

        public static Bitmap ResizeImage(Image image, Size newSize)
        {
            Bitmap resizedBitmap = new Bitmap(newSize.Width, newSize.Height);

            using (Graphics graphics = Graphics.FromImage(resizedBitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, new Rectangle(Point.Empty, newSize));
            }

            return resizedBitmap;
        }
    }
}
