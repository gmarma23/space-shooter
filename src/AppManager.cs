using SpaceShooter.gui;

namespace SpaceShooter
{
    internal static class AppManager
    {
        private static MenuFrame menuFrame = new MenuFrame();

        public static void Start() => Application.Run(menuFrame);

        public static void OnMenuOptionPlayClick(object sender, EventArgs e) 
        {
            menuFrame.Hide();
            GameManager.StartGame();
        }

        public static void OnMenuOptionHighscoresClick(object sender, EventArgs e)
        {
            menuFrame.Hide();
            new HighscoresFrame().Show();
        }

        public static void OnMenuOptionControlsClick(object sender, EventArgs e)
        {
            menuFrame.Hide();
            new ControlsFrame().Show();
        }

        public static void OnSubFrameClose(object sender, EventArgs e) => menuFrame.Show();
    }
}
