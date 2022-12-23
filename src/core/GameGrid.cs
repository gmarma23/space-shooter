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

        public int GetItemMinPossibleX()
        {
            return 0;
        }

        public int GetItemMinPossibleY()
        {
            return 0;
        }

        public int GetItemMaxPossibleX(IGridItem item)
        {
            return XDimension - item.Width;
        }

        public int GetItemMaxPossibleY(IGridItem item)
        {
            return YDimension - item.Height;
        }

        public bool IsValidLocation(IGridItem item, int newX, int newY)
        {
            return newX >= GetItemMinPossibleX() && 
                   newY >= GetItemMinPossibleY() &&
                   newX <= GetItemMaxPossibleX(item) &&
                   newY <= GetItemMaxPossibleY(item);
        }

        public static bool ItemsIntersect(IGridItem item1, IGridItem item2)
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
