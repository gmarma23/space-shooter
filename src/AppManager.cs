using SpaceShooter.gui;

namespace SpaceShooter
{
    internal static class AppManager
    {
        private static MenuForm menuForm = new MenuForm();

        public static void Start() => Application.Run(menuForm);

        public static void OnMenuOptionPlayClick(object? sender, EventArgs e) 
        {
            menuForm.Hide();
            GameManager.StartNewGame();
        }

        public static void OnMenuOptionHighscoresClick(object? sender, EventArgs e)
        {
            menuForm.Hide();
            new HighscoresForm().Show();
        }

        public static void OnMenuOptionOptionsClick(object? sender, EventArgs e)
        {
            menuForm.Hide();
            //new OptionsForm().Show();
        }

        public static void OnMenuOptionAboutClick(object? sender, EventArgs e)
        {
            menuForm.Hide();
            new AboutForm().Show();
        }

        public static void OnSubFormClosed(object? sender, EventArgs e) => menuForm.Show();
    }
}
