using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class GameGrid
    {
        public int XDimension { get; private init; }
        public int YDimension { get; private init; }

        public GameGrid(int xDimension, int yDimension) 
        { 
            XDimension = xDimension;
            YDimension = yDimension;
        }

        public bool IsValidLocation(IGridItem item, int newX, int newY)
        {
            return newX >= 0 && newY >= 0 &&
                newX + item.Width <= XDimension &&
                newY + item.Height <= YDimension;
        }

        public static bool GridItemsIntersect(IGridItem item1, IGridItem item2)
        {
            int deltaX = item2.XLocation - item1.XLocation;
            int deltaY = item2.YLocation - item1.YLocation;

            if (deltaX == 0 || deltaY == 0) return true;

            bool horizontalIntersect = deltaX > 0 ? deltaX <= item1.Width : - deltaX <= item2.Width;
            bool verticalIntersect = deltaY > 0 ? deltaY <= item1.Height : - deltaY <= item2.Height;

            return horizontalIntersect || verticalIntersect;
        }
    }
}
