using System.Collections.Generic;

namespace SpaceShooter.gui
{
    public partial class AboutForm : CustomForm
    {
        const float textLabelMarginRatio = 0.1f;

        public AboutForm()
        {
            InitializeComponent();
            setBackgroundImage();

            var titleLabel = new FormTitleLabel(this, "    ABOUT    ");

            int textLabelMargin = (int)(Height * textLabelMarginRatio);

            string gameDescription = " Exterminate hostile spaceships and\n survive as many waves as possible,\n aiming for the top of the highscores\n list.";
            var gameDescriptionLabel = new TextLabel(this, gameDescription)
            {
                Top = titleLabel.Top + titleLabel.Height + textLabelMargin
            };

            string controls = " Use arrow keys or WASD keys to move\n hero and space bar to fire laser blaster.";

            var controlsLabel = new TextLabel(this, controls)
            {
                Top = gameDescriptionLabel.Top + gameDescriptionLabel.Height + textLabelMargin
            };
        }
    }
}
