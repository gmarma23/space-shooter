using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.core
{
    internal abstract class Spaceship
    {
        public int TotalHP { get; init; }
        public int AvailableHP { get; protected set; }
        public int XLocation { get; protected set; }
        public int YLocation { get; protected set; }
        public int XDisplacement { get; protected set; }
        public int YDisplacement { get; protected set; }
        public int XVelocity { get; protected set; }
        public int YVelocity { get; protected set; }
        public int ConcurrentLaserBlastCount { get; protected set; }
        public int LaserBlastDamage { get; protected set; }
        public int LaserReload { get; protected set; }
        public int MissileCount { get; protected set; }
        public int MissileDamage { get; protected set; }
        public int MissileReload { get; protected set; }

        public abstract void TakeDamage(int damage);

        public abstract void RestoreHealth(int health);
    }
}
