using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.core
{
    public abstract class Weapon : CollidableItem
    {
        protected static readonly CachedSound hitImpactSoundFx = new(Resources.aud_hit_impact);

        public Weapon() { }

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
                AudioPlayer.Player.PlaySound(hitImpactSoundFx);
                Target.TakeDamage(damage);
                IsActive = false;
            }
        }
    }
}
