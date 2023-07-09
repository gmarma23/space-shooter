namespace SpaceShooter.gui
{
    public partial class AboutForm : CustomForm
    {
        private const float textLabelMarginRatio = 0.1f;
        private const float developerLabelWidthRatio = 0.3f;
        private const float developerLabelHeightRatio = 0.8f;
        private const float okBtnHeightRatio = 0.077f;
        private const float okBtnWidthRatio = 0.2f;

        public AboutForm()
        {
            InitializeComponent();
            setBackgroundImage();

            FormTitleLabel titleLabel = new FormTitleLabel(this, new string(' ', 4) + "ABOUT" + new string(' ', 4));

            int textLabelMargin = (int)(Height * textLabelMarginRatio);

            string gameDescription = " Exterminate hostile spaceships and\n survive as many waves as possible,\n aiming for the top of the highscores\n list.";
            TextLabel gameDescriptionLabel = new TextLabel(this, gameDescription)
            {
                Top = titleLabel.Top + titleLabel.Height + textLabelMargin
            };

            string developer = new string(' ', 12) + "Created by gmarma23" + new string(' ', 12);

            CustomLinkLabel devloperLabel = new CustomLinkLabel(this, developer, developerLabelWidthRatio, developerLabelHeightRatio)
            {
                Parent = this,
                LinkArea = new LinkArea(23, 31),
                VisitedLinkColor = Color.White,
                LinkColor = Color.White,
                Top = gameDescriptionLabel.Top + gameDescriptionLabel.Height + textLabelMargin
            };
            devloperLabel.LinkClicked += onDeveloperLinkAreaClick;

            var okBtn = new OKButton(this, okBtnWidthRatio, okBtnHeightRatio)
            {
                Top = devloperLabel.Top + devloperLabel.Height + textLabelMargin,
            };
        }

        private void onDeveloperLinkAreaClick(object? sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/gmarma23");
        }
    }
}
