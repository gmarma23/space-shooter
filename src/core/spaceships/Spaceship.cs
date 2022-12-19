using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal class Spaceship
    {
        public int TotalHP { get; init; }
        public int AvailableHP { get; private set; }
        public int LaserBlastCount { get; private set; }
        public int LaserBlastDamage { get; private set; }
        public int XDisplacement { get; private set; }
        public int YDisplacement { get; private set; }
    }
}
