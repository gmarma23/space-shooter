using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal interface IGridItem
    {
        int LocationX { get; }
        int LocationY { get; }
        int DisplacementX { get; }
        int DisplacementY { get; }
        int Width { get; }
        int Height { get; }
    }
}
