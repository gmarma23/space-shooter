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
            
            BackgroundImage = resources.Resources.img_space_background_v;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Color.Black;

            Text = "Space Shooter";
            Icon = resources.Resources.ico_space_shooter;
        }
    }
}
