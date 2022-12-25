using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.gui
{
    internal abstract class SpaceshipGui : Panel
    {
        protected const double healthBarHeightRatio = 0.05;
        protected const double healthBarMarginRatio = 0.07;

        protected SpaceshipHealthBar spaceshipHealthBar;

        public SpaceshipGui(int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) 
        {
            Width = spaceshipPicBoxWidth;
            Height = spaceshipPicBoxHeight + (int)((healthBarHeightRatio + healthBarMarginRatio) * spaceshipPicBoxHeight);

            int healthBarWidth = spaceshipPicBoxWidth;
            int healthBarHeight = (int)(spaceshipPicBoxHeight * healthBarHeightRatio);

            spaceshipHealthBar = new SpaceshipHealthBar(healthBarWidth, healthBarHeight);
            Controls.Add(spaceshipHealthBar);
        }

        public void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY)
        {
            Location = new Point(newSpaceshipLocationX, newSpaceshipLocationY);
        }

        protected abstract void arrangeItems();
    }
}
