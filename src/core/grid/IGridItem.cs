using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal interface IGridItem
    {
        int XLocation { get; }
        int YLocation { get; }
        int XDisplacement { get; }
        int YDisplacement { get; }
        int Width { get; }
        int Height { get; }
    }
}
