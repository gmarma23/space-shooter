using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.core
{
    public class HealthKit : CollidableItem
    {
        protected static readonly CachedSound healthBoostSoundFx = new(Resources.aud_health_boost);
        protected int health;

        public HealthKit(GameGrid grid, int health = 50)
        {
            this.health = health;

            defaultWidthRatio = 0.05f;
            defaultHeightRatio = 1;
            absMaxDisplacement = 420;

            setSize(grid);
            setBounds(grid);

            Image = Resources.img_health_kit;
        }

        protected override void checkTargetCollided()
        {
            if (Target == null)
                return;

            if (!Target.IsActive)
            {
                Target = null;
                return;
            }

            if (GameGrid.ItemsIntersect(this, Target))
            {
                AudioPlayer.Player.PlaySound(healthBoostSoundFx);
                Target.RestoreHealth(health);
                IsActive = false;
            }
        }
    }
}
