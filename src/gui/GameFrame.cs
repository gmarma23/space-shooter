using SpaceShooter.core;
using System.Diagnostics;

namespace SpaceShooter.gui
{
    internal partial class GameFrame : CustomFrame
    {
        private HeroSpaceshipGui hero;
        private EnemySpaceshipGui enemy;
        private List<WeaponGui> activeWeaponsGui;

        public GameFrame(int gridDimensionX, int gridDimensionY, Dictionary<string, KeyEventHandler> keyEventHandlers)
        {
            InitializeComponent();
            ClientSize = new Size(gridDimensionX, gridDimensionY);

            activeWeaponsGui = new List<WeaponGui>();

            FormClosed += AppManager.OnSubFrameClose;
            KeyDown += keyEventHandlers["OnKeyDown"];
            KeyUp += keyEventHandlers["OnKeyUp"];
        }

        public void RelocateSpaceship(IGameStateUI gameState, bool isHero)
        {
            IHPGridItem spaceship = gameState.GetSpaceshipToDraw(isHero);
            Debug.Assert(spaceship != null);

            SpaceshipGui spaceshipGui = getSpaceshipGui(isHero);
            Debug.Assert(spaceshipGui != null);

            spaceshipGui.UpdateLocation(spaceship.LocationX, spaceship.LocationY);
        }

        public void UpdateActiveWeapons(IGameStateUI gameState)
        {
            Debug.Assert(activeWeaponsGui != null);
            List<int> activeWeaponIDsGui = activeWeaponsGui.Select(weaponGui => weaponGui.ID).ToList();
            List<int> activeWeaponIDs = gameState.GetActiveWeaponIDs();

            List<int> newWeaponIDs = activeWeaponIDs.Except(activeWeaponIDsGui).ToList();
            List<int> disposedWeaponIDs = activeWeaponIDsGui.Except(activeWeaponIDs).ToList();

            disposeInactiveWeapons(disposedWeaponIDs);
            renderNewWeapons(gameState, newWeaponIDs);
            relocateExistingWeapons(gameState);
        }

        public void UpdateSpaceshipAvailableHealth(IGameStateUI gameState, bool isHero)
        {
            IHPGridItem spaceship = gameState.GetSpaceshipToDraw(isHero);
            Debug.Assert(spaceship != null);

            SpaceshipGui spaceshipGui = getSpaceshipGui(isHero);
            Debug.Assert(spaceshipGui != null);

            float availableHealthRatio = spaceship.GetAvailableHealthRatio();
            spaceshipGui.UpdateAvailableHealth(availableHealthRatio);
        }

        public void RenderHeroSpaceship(IGameStateUI gameState)
        {
            IHPGridItem heroSpaceship = gameState.GetSpaceshipToDraw(true);
            Debug.Assert(heroSpaceship != null);

            hero = new HeroSpaceshipGui(heroSpaceship.Width, heroSpaceship.Height, heroSpaceship.Image);
            Controls.Add(hero);
        }

        public void RenderEnemySpaceship(IGameStateUI gameState)
        {
            IHPGridItem enemySpaceship = gameState.GetSpaceshipToDraw(false);
            Debug.Assert(enemySpaceship != null);

            enemy = new EnemySpaceshipGui(enemySpaceship.Width, enemySpaceship.Height, enemySpaceship.Image);
            Controls.Add(enemy);
        }

        public async Task DestroySpaceship(bool isHero)
        {
            SpaceshipGui spaceshipGui = getSpaceshipGui(isHero);
            Debug.Assert(spaceshipGui != null);

            await spaceshipGui.Explode();
            spaceshipGui.DisposePictureBox();
        }

        private void relocateExistingWeapons(IGameStateUI gameState)
        {
            foreach(var weaponGui in activeWeaponsGui)
            {
                IGridItem? weapon = gameState.GetWeaponToDraw(weaponGui.ID);
                Debug.Assert(weapon != null);

                weaponGui.UpdateLocation(weapon.LocationX, weapon.LocationY);
            }
        }

        private void renderNewWeapons(IGameStateUI gameState, List<int> newWeaponIDs)
        {
            foreach(var id in newWeaponIDs)
            {
                IGridItem? weapon = gameState.GetWeaponToDraw(id);
                Debug.Assert(weapon != null);

                WeaponGui newWeaponGui = new WeaponGui(id, weapon.Width, weapon.Height, weapon.Image);
                activeWeaponsGui.Add(newWeaponGui);
                Controls.Add(newWeaponGui);
            }
        }

        private void disposeInactiveWeapons(List<int> disposedWeaponIDs)
        {
            for (int i = disposedWeaponIDs.Count - 1; i >= 0; i--)
            {
                int id = disposedWeaponIDs[i];
                WeaponGui? weaponGui = activeWeaponsGui.Find(laserBlast => laserBlast.ID == id);
                Debug.Assert(weaponGui != null);

                activeWeaponsGui.Remove(weaponGui);

                Image weaponImage = weaponGui.Image;
                weaponGui.Image = null;
                weaponImage.Dispose();
                weaponGui.Dispose();
            }
        }

        public SpaceshipGui getSpaceshipGui(bool isHero) => isHero ? hero : enemy;
    }
}
