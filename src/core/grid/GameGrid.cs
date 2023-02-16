using System.Diagnostics;

namespace SpaceShooter.core
{
    public class GameGrid
    {
        public int DimensionX { get; private init; }
        public int DimensionY { get; private init; }

        public GameGrid(int dimensionX, int dimensionY) 
        { 
            DimensionX = dimensionX;
            DimensionY = dimensionY;
        }

        public int GetDimensionAverage() => (DimensionX + DimensionY) / 2;

        public int GetGridMiddleY() => DimensionY / 2;

        public int GetItemMinPossibleX() => 0;

        public int GetItemMinPossibleY() => 0;

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

        public static bool ItemsIntersect(IGridItem item1, IGridItem item2)
        {
            Debug.Assert(item1 != null && item2 != null);

            int deltaX = item2.LocationX - item1.LocationX;
            int deltaY = item2.LocationY - item1.LocationY;
            
            IGridItem minLocationXItem = deltaX >= 0 ? item1 : item2;
            IGridItem minLocationYItem = deltaY >= 0 ? item1 : item2;

            bool horizontalIntersect = Math.Abs(deltaX) < minLocationXItem.Width;
            bool verticalIntersect = Math.Abs(deltaY) < minLocationYItem.Height;

            return horizontalIntersect && verticalIntersect;
        }
    }
}
