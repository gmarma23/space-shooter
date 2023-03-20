namespace SpaceShooter.gui
{
    public partial class HighscoresForm : CustomForm
    {
        
        public HighscoresForm()
        {
            InitializeComponent();

            FormClosed += AppManager.OnSubFormClosed;

            new FormTitleLabel(this)
            {
                Text = "HIGHSCORES"
            };
        }
    }
}
