namespace SpaceShooter.gui
{
    public class SpaceshipHealthBar : Panel
    {
        private readonly Panel totalHealthBar;
        private readonly Panel availableHealthBar;

        public SpaceshipHealthBar(int width, int height) 
        {
            Width = width;
            Height = height;

            availableHealthBar = new Panel();
            setSubBar(availableHealthBar, true);

            totalHealthBar = new Panel();
            setSubBar(totalHealthBar, false);
        }

        public void UpdateAvailableHealth(float availableHealthPercentage)
            => availableHealthBar.Width = (int)(totalHealthBar.Width * availableHealthPercentage);

        private void setSubBar(Panel subBar, bool isAvailableHealthBar)
        {
            Controls.Add(subBar);
            subBar.Width = Width;
            subBar.Height = Height;
            subBar.BackColor = isAvailableHealthBar ? Color.Green : Color.Red;
            if (isAvailableHealthBar) 
                subBar.BringToFront();
        }
    }
}
