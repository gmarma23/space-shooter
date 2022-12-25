namespace SpaceShooter.gui
{
    internal class SpaceshipHealthBar : Panel
    {
        private readonly Panel totalHealthBar;
        private Panel availableHealthBar;

        public SpaceshipHealthBar(int width, int height) 
        {
            Width = width;
            Height = height;

            availableHealthBar = new Panel();
            setSubBar(availableHealthBar, false);

            totalHealthBar = new Panel();
            setSubBar(totalHealthBar, true);
        }

        public void UpdateHealth(double availableHealthPercentage)
        {
            availableHealthBar.Width = (int)(totalHealthBar.Width * availableHealthPercentage);
        }

        private void setSubBar(Panel subBar, bool isAvailableHealthBar)
        {
            subBar.Width = Width;
            subBar.Height = Height;
            subBar.BackColor = isAvailableHealthBar ? Color.Green : Color.Red;
            subBar.Parent = isAvailableHealthBar ? totalHealthBar : this;
            subBar.Location = new Point(0, 0);
        }
    }
}
