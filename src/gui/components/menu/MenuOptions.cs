using System.Diagnostics;

namespace SpaceShooter.gui
{
    internal class MenuOptions : Panel
    {
        public const int menuButtonMarginRatio = 4;

        private readonly string[] menuButtonsText =
        {
            "Play",
            "Highscores",
            "Controls"
        };

        private readonly EventHandler[] menuButtonsEventHandlers =
        {
            AppManager.OnMenuOptionPlayClick,
            AppManager.OnMenuOptionHighscoresClick,
            AppManager.OnMenuOptionControlsClick
        };

        private MenuButton[] menuButtons;
        private readonly int numOfButtons;

        public int MenuButtonHeight { get; private set; }
        private int menuButtonsVerticalMargin;

        public MenuOptions(MenuFrame menuFrame)
        {
            Parent = menuFrame;

            Debug.Assert(menuButtonsText.Length == menuButtonsEventHandlers.Length);
            numOfButtons = menuButtonsText.Length;

            Width = (int)(menuFrame.Width * MenuFrame.optionsWidthRatio);
            Height = (int)(menuFrame.Height * MenuFrame.optionsHeightRatio);

            calculateButtonAndMarginSizes();

            Height -= menuButtonsVerticalMargin;

            Location = new Point(
                menuFrame.ClientRectangle.Width / 2 - Width / 2, 
                (int)(menuFrame.ClientRectangle.Height * MenuFrame.optionsLocationYRatio) - Height / 2);

            initializeMenuButtons(); 
            arrangeMenuButtons();
        }

        private void calculateButtonAndMarginSizes()
        {
            int combinedHeight = Height / numOfButtons;
            menuButtonsVerticalMargin = combinedHeight / menuButtonMarginRatio;
            MenuButtonHeight = combinedHeight - menuButtonsVerticalMargin;
        }

        private void arrangeMenuButtons()
        {
            for (int i = 0; i < menuButtons.GetLength(0); i++)
                menuButtons[i].Top = i * (MenuButtonHeight + menuButtonsVerticalMargin);
        }

        private void initializeMenuButtons()
        {
            menuButtons = new MenuButton[numOfButtons];
            for (int i = 0; i < menuButtons.GetLength(0); i++)
                menuButtons[i] = new MenuButton(this, menuButtonsText[i], menuButtonsEventHandlers[i]);  
        }
    }
}
