namespace SpaceShooter.gui
{
    public partial class AboutForm : CustomForm
    {
        public AboutForm()
        {
            InitializeComponent();
            FormClosed += AppManager.OnSubFormClosed;
        }
    }
}
