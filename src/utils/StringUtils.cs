namespace SpaceShooter.utils
{
    public static class StringUtils
    {
        public static string FormatSecondsToHMS(double totalSeconds)
        {
            int seconds = (int)Math.Floor(totalSeconds);

            int minutes = seconds / 60;
            seconds %= 60;

            int hours = minutes / 60;
            minutes %= 60;

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        } 
    }
}
