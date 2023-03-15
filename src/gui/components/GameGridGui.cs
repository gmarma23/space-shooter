using SpaceShooter.core;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceShooter.gui
{
    public class GameGridGui : Panel
    {
        private HeroSpaceshipGui hero;
        private EnemySpaceshipGui enemy;
        private List<WeaponGui> activeWeaponsGui;

        public GameGridGui(Control parent, IGameStateUI gameState)
        {
            Width = gameState.Grid.DimensionX;
            Height = gameState.Grid.DimensionY;
            Parent = parent;

            activeWeaponsGui = new List<WeaponGui>();
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

            RelocateSpaceship(gameState, true);
        }

        public void RenderEnemySpaceship(IGameStateUI gameState)
        {
            IHPGridItem enemySpaceship = gameState.GetSpaceshipToDraw(false);
            Debug.Assert(enemySpaceship != null);

            enemy = new EnemySpaceshipGui(enemySpaceship.Width, enemySpaceship.Height, enemySpaceship.Image);
            Controls.Add(enemy);

            RelocateSpaceship(gameState, false);
        }

        public async Task DestroySpaceship(bool isHero)
        {
            SpaceshipGui spaceshipGui = getSpaceshipGui(isHero);
            Debug.Assert(spaceshipGui != null);

            await spaceshipGui.Explode();
            spaceshipGui.DisposePictureBox();
            Controls.Remove(spaceshipGui);
        }

        public void GameOverActions()
        {
            if (Controls.Contains(hero))
            {
                hero.DisposePictureBox();
                Controls.Remove(hero);
            }

            if (Controls.Contains(enemy))
            {
                enemy.DisposePictureBox();
                Controls.Remove(enemy);
            }

            for (int i = activeWeaponsGui.Count - 1; i >= 0; i--)
            {
                activeWeaponsGui[i].DisposeImage();
                Controls.Remove(activeWeaponsGui[i]);
                activeWeaponsGui.RemoveAt(i);
            }

            PrintGameOverMessage();
        }

        private void relocateExistingWeapons(IGameStateUI gameState)
        {
            foreach (var weaponGui in activeWeaponsGui)
            {
                IGridItem? weapon = gameState.GetWeaponToDraw(weaponGui.ID);
                Debug.Assert(weapon != null);

                weaponGui.UpdateLocation(weapon.LocationX, weapon.LocationY);
            }
        }

        private void renderNewWeapons(IGameStateUI gameState, List<int> newWeaponIDs)
        {
            foreach (var id in newWeaponIDs)
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

                weaponGui.DisposeImage();
                Controls.Remove(weaponGui);
            }
        }

        private void PrintGameOverMessage()
        {
            Label gameOverMessage = new Label();
            Controls.Add(gameOverMessage);

            gameOverMessage.AutoSize = true;
            gameOverMessage.BackColor = Color.Transparent;
            gameOverMessage.Font = new Font(
                "Microsoft Sans Serif",
                40.0F, FontStyle.Bold | FontStyle.Italic,
                GraphicsUnit.Point
            );
            gameOverMessage.ForeColor = Color.DarkRed;
            gameOverMessage.Text = "GAME OVER!";
            gameOverMessage.TextAlign = ContentAlignment.MiddleCenter;
            gameOverMessage.UseCompatibleTextRendering = true;
            gameOverMessage.Location = new Point(
                Width / 2 - gameOverMessage.Width / 2,
                Height / 2 - gameOverMessage.Height / 2
            );
        }

        public SpaceshipGui getSpaceshipGui(bool isHero) => isHero ? hero : enemy;
    }
}