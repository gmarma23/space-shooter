using SpaceShooter.resources;

namespace SpaceShooter.gui
{
    public partial class CustomForm : Form
    {
        public CustomForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            AutoSize = false;
            Size = new Size(400, 500);
            MaximizeBox = false;
            
            BackColor = Color.Black;

            Text = "Space Shooter";
            Icon = resources.Resources.ico_space_shooter;

            FormClosing += disposeBackgroundImage;
        }

        protected void setBackgroundImage()
        {
            bool isFormHorizontal = Width >= Height;
            BackgroundImage = isFormHorizontal ? Resources.img_space_background : Resources.img_space_background_v;
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        protected void disposeBackgroundImage(object? sender, EventArgs e)
        {
            Image? backgroundImage = BackgroundImage;
            if (backgroundImage == null)
                return;

            BackgroundImage = null;
            backgroundImage.Dispose();
        }
    }
}
