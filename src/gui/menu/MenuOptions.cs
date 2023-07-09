using System.Diagnostics;
using SpaceShooter.src.gui;

namespace SpaceShooter.gui
{
    internal class MenuOptions : Panel
    {
        public const int menuButtonMarginRatio = 4;

        private const float optionsLocationYRatio = 0.77f;
        private const float optionsWidthRatio = 0.32f;
        private const float optionsHeightRatio = 0.35f;

        private readonly string[] menuButtonsText =
        {
            "Play",
            "Highscores",
            "Options",
            "About"
        };

        private readonly EventHandler[] menuButtonsEventHandlers =
        {
            AppManager.OnMenuOptionPlayClick,
            AppManager.OnMenuOptionHighscoresClick,
            AppManager.OnMenuOptionOptionsClick,
            AppManager.OnMenuOptionAboutClick
        };

        private CustomButton[] menuButtons;
        private readonly int numOfButtons;

        private int menuButtonsVerticalMargin;
        private int menuButtonHeight;

        public MenuOptions(Control parent)
        {
            Parent = parent;

            Debug.Assert(menuButtonsText.Length == menuButtonsEventHandlers.Length);
            numOfButtons = menuButtonsText.Length;

            Width = (int)(parent.Width * optionsWidthRatio);
            Height = (int)(parent.Height * optionsHeightRatio);
            BackColor = Color.Transparent;

            calculateButtonAndMarginSizes();

            Height -= menuButtonsVerticalMargin;

            Location = new Point(
                parent.ClientRectangle.Width / 2 - Width / 2, 
                (int)(parent.ClientRectangle.Height * optionsLocationYRatio) - Height / 2);

            initializeMenuButtons(); 
            arrangeMenuButtons();
        }

        private void calculateButtonAndMarginSizes()
        {
            int combinedHeight = Height / numOfButtons;
            menuButtonsVerticalMargin = combinedHeight / menuButtonMarginRatio;
            menuButtonHeight = combinedHeight - menuButtonsVerticalMargin;
        }

        private void arrangeMenuButtons()
        {
            for (int i = 0; i < menuButtons.GetLength(0); i++) 
                menuButtons[i].Top = i * (menuButtonHeight + menuButtonsVerticalMargin);
        }

        private void initializeMenuButtons()
        {
            menuButtons = new CustomButton[numOfButtons];
            for (int i = 0; i < menuButtons.GetLength(0); i++)
            {
                menuButtons[i] = new CustomButton(this, menuButtonsText[i], 1, (float)((float)menuButtonHeight / (float)Height));
                menuButtons[i].Click += menuButtonsEventHandlers[i];
            }
        }
    }
}
