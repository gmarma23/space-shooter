namespace SpaceShooter.utils
{
    public static class ScreenUtils
    {
        public static Screen GetLargestScreen()
        {
            Screen largestScreen = Screen.PrimaryScreen;

            foreach (Screen screen in Screen.AllScreens)
                if (screen.Bounds.Width * screen.Bounds.Height > largestScreen.Bounds.Width * largestScreen.Bounds.Height)
                    largestScreen = screen;

            return largestScreen;
        }
    }
}
