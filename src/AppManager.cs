using SpaceShooter.gui;
using System.Diagnostics;
using System.Reflection;

namespace SpaceShooter
{
    internal static class AppManager
    {
        private static MenuForm menuForm = new MenuForm();

        public static void Start() => Application.Run(menuForm);

        public static void OnMenuOptionPlayClick(object sender, EventArgs e) 
        {
            menuForm.Hide();
            GameManager.StartNewGame();
        }

        public static void OnMenuOptionHighscoresClick(object sender, EventArgs e)
        {
            menuForm.Hide();
            new HighscoresForm().Show();
        }

        public static void OnMenuOptionControlsClick(object sender, EventArgs e)
        {
            menuForm.Hide();
            new ControlsForm().Show();
        }

        public static void OnSubFormClosed(object sender, EventArgs e) => menuForm.Show();
    }
}
