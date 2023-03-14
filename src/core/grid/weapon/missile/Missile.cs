using System;

namespace SpaceShooter.core
{
    public abstract class Missile : Weapon
    {
        public Missile(ILaunchMissile missileLauncher, GameGrid grid) 
        {
            defaultWidthRatio = 0.03f;
            defaultHeightRatio = 2;
            absMaxDisplacement = 360;

            setSize(grid);
            setBounds(grid);

            LocationX = missileLauncher.LocationX + (missileLauncher.Width / 2) - (Width / 2);
            damage = missileLauncher.MissileDamage;
        }

        public override void Move()
        {
            updateDisplacementX();
            base.Move();
        }

        protected void updateDisplacementX()
        {
            if (Target == null)
                return;

            int deltaLocationX = Target.LocationX - LocationX;
            int nexDisplacementX = Math.Sign(deltaLocationX) * absMaxDisplacement;
            if (Math.Abs(deltaLocationX) >= nexDisplacementX)
                displacementX = nexDisplacementX;
            else
                displacementX = deltaLocationX;
        }
    }
}
