using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.core
{
    public class HealthKit : PowerUp
    {
        protected static readonly CachedSound healthBoostSoundFx = new(Resources.aud_health_boost);
        protected readonly int health;

        public HealthKit(GameGrid grid, IGridItem source, int health = 50) : base(grid, source)
        {
            this.health = health;
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
