using SpaceShooter.src.gui;
using System.Collections.Generic;
using System.Security.Policy;

namespace SpaceShooter.gui
{
    public partial class AboutForm : CustomForm
    {
        private const float textLabelMarginRatio = 0.1f;
        private const float okBtnHeightRatio = 0.085f;
        private const float okBtnWidthRatio = 0.2f;

        public AboutForm()
        {
            InitializeComponent();
            setBackgroundImage();

            var titleLabel = new FormTitleLabel(this, new string(' ', 4) + "ABOUT" + new string(' ', 4));

            int textLabelMargin = (int)(Height * textLabelMarginRatio);

            string gameDescription = " Exterminate hostile spaceships and\n survive as many waves as possible,\n aiming for the top of the highscores\n list.";
            var gameDescriptionLabel = new TextLabel(this, gameDescription)
            {
                Top = titleLabel.Top + titleLabel.Height + textLabelMargin
            };

            string developer = new string(' ', 12) + "Created by gmarma23" + new string(' ', 12);

            var aboutDevloper = new CustomLinkLabel(this, developer, 0.3f, 0.8f)
            {
                Parent = this,
                LinkArea = new LinkArea(23, 31),
                VisitedLinkColor = Color.White,
                LinkColor = Color.White,
                Top = gameDescriptionLabel.Top + gameDescriptionLabel.Height + textLabelMargin
            };
            aboutDevloper.LinkClicked += onDeveloperLinkAreaClick;

            var okBtn = new CustomButton(this)
            {
                Width = (int)(Width * okBtnWidthRatio),
                Height = (int)(Height * okBtnHeightRatio),
                Top = aboutDevloper.Top + aboutDevloper.Height + textLabelMargin,
                Text = "OK"
            };
            okBtn.Left = ClientRectangle.Width / 2 - okBtn.Width / 2;
            okBtn.Click += (sender, e) => Close();
        }

        private void onDeveloperLinkAreaClick(object? sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/gmarma23");
        }
    }
}
