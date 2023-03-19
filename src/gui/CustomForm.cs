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
            MaximizeBox = false;
            BackColor = Color.Black;
            Size = new Size(400, 500);

            Text = "Space Shooter";
            Icon = resources.Resources.ico_space_shooter;
        }
    }
}
