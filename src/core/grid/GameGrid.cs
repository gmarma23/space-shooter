using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class GameGrid
    {
        public int DimensionX { get; private init; }
        public int DimensionY { get; private init; }

        public GameGrid(int dimensionX, int dimensionY) 
        { 
            DimensionX = dimensionX;
            DimensionY = dimensionY;
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
            Debug.Assert(item != null);
            return DimensionX - item.Width;
        }

        public int GetItemMaxPossibleY(IGridItem item)
        {
            Debug.Assert(item != null);
            return DimensionY - item.Height;
        }

        public bool IsValidLocation(IGridItem item, int newX, int newY)
        {
            Debug.Assert(item != null);

            bool isValidNewX = newX >= GetItemMinPossibleX() && newX <= GetItemMaxPossibleX(item);
            bool isValidNewY = newY >= GetItemMinPossibleY() && newY <= GetItemMaxPossibleY(item);

            return isValidNewX && isValidNewY;
        }

        public static bool ItemsIntersect(IGridItem item1, IGridItem item2)
        {
            Debug.Assert(item1 != null);
            Debug.Assert(item2 != null);

            int dx = item2.LocationX - item1.LocationX;
            int dy = item2.LocationY - item1.LocationY;

            if (dx == 0 || dy == 0) return true;

            bool horizontalIntersect = dx > 0 ? dx <= item1.Width : - dx <= item2.Width;
            bool verticalIntersect = dy > 0 ? dy <= item1.Height : - dy <= item2.Height;

            return horizontalIntersect || verticalIntersect;
        }

        public int GetHorizontallyCenteredItemX(IGridItem item)
        {
            return DimensionX / 2 - item.Width / 2;
        }
    }
}
