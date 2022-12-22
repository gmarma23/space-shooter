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
            int dx = item2.XLocation - item1.XLocation;
            int dy = item2.YLocation - item1.YLocation;

            if (dx == 0 || dy == 0) return true;

            bool horizontalIntersect = dx > 0 ? dx <= item1.Width : - dx <= item2.Width;
            bool verticalIntersect = dy > 0 ? dy <= item1.Height : - dy <= item2.Height;

            return horizontalIntersect || verticalIntersect;
        }
    }
}
