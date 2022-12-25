using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.gui
{
    internal class SpaceshipPanel : Panel
    {
        private const double healthBarHeightRatio = 0.05;
        private const double healthBarMarginRatio = 0.05;

        private SpaceshipPictureBox spaceshipPictureBox;
        private SpaceshipHealthBar spaceshipHealthBar;

        public SpaceshipPanel(SpaceshipType type, int spaceshipPicBoxWidth, int spaceshipPicBoxHeight) 
        {
            Width = spaceshipPicBoxWidth;
            Height = spaceshipPicBoxHeight + (int)((healthBarHeightRatio + healthBarMarginRatio) * spaceshipPicBoxHeight);

            spaceshipPictureBox = new SpaceshipPictureBox(type, spaceshipPicBoxWidth, spaceshipPicBoxHeight);
            Controls.Add(spaceshipPictureBox);
            spaceshipPictureBox.Parent = this;
            spaceshipPictureBox.Top = (type == SpaceshipType.Hero) ? 0 : (int)((healthBarHeightRatio + healthBarMarginRatio) * spaceshipPicBoxHeight);
            
            int healthBarWidth = spaceshipPicBoxWidth;
            int healthBarHeight = (int)(spaceshipPicBoxHeight * healthBarHeightRatio);
            spaceshipHealthBar = new SpaceshipHealthBar(healthBarWidth, healthBarHeight);
            Controls.Add(spaceshipHealthBar);
            spaceshipHealthBar.Parent = this;
            spaceshipHealthBar.Top = (type == SpaceshipType.Hero) ? (int)((1 + healthBarMarginRatio) * spaceshipPictureBox.Height) : 0;
        }

        public void UpdateLocation(int newSpaceshipLocationX, int newSpaceshipLocationY)
        {
            Location = new Point(newSpaceshipLocationX, newSpaceshipLocationY);
        }
    }
}
