using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.src.core.spaceships
{
    internal interface ILaunchMissile
    {
        public int MissileCount { get; }
        public int MissileDamage { get; }
        public int MissileReloadTime { get; }
        public bool MissileIsReloading { get; set; }
    }
}
