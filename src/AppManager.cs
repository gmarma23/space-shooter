using SpaceShooter.gui;
using System.Diagnostics;
using System.Reflection;

namespace SpaceShooter
{
    internal static class AppManager
    {
        private static MenuFrame menuFrame = new MenuFrame();

        public static void Start()
        {
            /*
            string dependencyFilename = "SQLite.Interop.dll";
            if (isDependecyMissing(dependencyFilename))
            {
                MessageBox.Show(
                    $"{dependencyFilename} not found!", 
                    "Missing Dependencies", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                return;
            }
            */
            Application.Run(menuFrame);
        }
            

        public static void OnMenuOptionPlayClick(object sender, EventArgs e) 
        {
            menuFrame.Hide();
            GameManager.StartNewGame();
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

        public static void OnSubFrameClosed(object sender, EventArgs e) => menuFrame.Show();

        private static bool isDependecyMissing(string dependencyFilename)
        {
            string pathToExecutable = Assembly.GetExecutingAssembly().Location;
            string? parentDirectory = Path.GetDirectoryName(pathToExecutable);
            Debug.Assert(parentDirectory != null);
            string pathToDependency = Path.Combine(parentDirectory, dependencyFilename);
            return !File.Exists(pathToDependency);
        }
    }
}
