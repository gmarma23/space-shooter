﻿namespace SpaceShooter.gui
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
            setSubBar(availableHealthBar, true);

            totalHealthBar = new Panel();
            setSubBar(totalHealthBar, false);
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
            subBar.Parent = this;
            subBar.Location = new Point(0, 0);
            if (isAvailableHealthBar) subBar.BringToFront();
        }
    }
}
