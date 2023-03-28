namespace SpaceShooter.gui
{
    public partial class AboutForm : CustomForm
    {
        public AboutForm()
        {
            InitializeComponent();
            setBackgroundImage();

            new FormTitleLabel(this, "ABOUT");
        }
    }
}
