using SpaceShooter.gui;

namespace SpaceShooter.src.gui.options
{
    public partial class OptionsForm : CustomForm
    {
        private const float okBtnHeightRatio = 0.05f;
        private const float okBtnWidthRatio = 0.178f;

        public OptionsForm(bool isPauseMenuInstance = false)
        {
            InitializeComponent();
            setBackgroundImage();

            if (isPauseMenuInstance)
                FormClosed -= AppManager.OnSubFormClosed;

            new FormTitleLabel(this, new string(' ', 5) + "OPTIONS" + new string(' ', 5));

            var okBtn = new CustomButton(this)
            {
                Width = (int)(Width * okBtnWidthRatio),
                Height = (int)(Height * okBtnHeightRatio),
                Top = 625,//aboutDevloper.Top + aboutDevloper.Height + textLabelMargin,
                Text = "OK"
            };
            okBtn.Left = ClientRectangle.Width / 2 - okBtn.Width / 2;
            okBtn.Click += (sender, e) => Close();
        }
    }
}
