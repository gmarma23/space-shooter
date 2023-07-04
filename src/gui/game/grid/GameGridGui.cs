using SpaceShooter.core;
using SpaceShooter.src.gui;
using SpaceShooter.src.gui.game.grid;
using System.Diagnostics;

namespace SpaceShooter.gui
{
    public class GameGridGui : Panel
    {
        private HeroSpaceshipGui hero;
        private EnemySpaceshipGui enemy;
        private readonly List<CollidableItemGui> activeCollidableItemsGui;
        private readonly GamePausedPanel pausedPanel;

        public GameGridGui(Control parent, IGameStateUI gameState, EventHandler onGameResume)
        {
            Width = gameState.Grid.DimensionX;
            Height = gameState.Grid.DimensionY;
            Parent = parent;

            BackgroundImage = resources.Resources.img_space_background;
            BackgroundImageLayout = ImageLayout.Stretch;
            DoubleBuffered = true;

            activeCollidableItemsGui = new List<CollidableItemGui>();
            pausedPanel = new GamePausedPanel(this, (GameForm)Parent, onGameResume);
        }

        public void RelocateSpaceship(IGameStateUI gameState, bool isHero)
        {
            IHPGridItem spaceship = gameState.GetSpaceshipToDraw(isHero);
            Debug.Assert(spaceship != null);

            SpaceshipGui spaceshipGui = getSpaceshipGui(isHero);
            Debug.Assert(spaceshipGui != null);

            spaceshipGui.UpdateLocation(spaceship.LocationX, spaceship.LocationY);
        }

        public void UpdateActiveCollidableItems(IGameStateUI gameState)
        {
            Debug.Assert(activeCollidableItemsGui != null);
            List<int> activeCollidableItemIDsGui = activeCollidableItemsGui.Select(item => item.ID).ToList();
            List<int> activeCollidableItemIDs = gameState.GetActiveCollidableItemIDs();

            List<int> newCollidableItemIDs = activeCollidableItemIDs.Except(activeCollidableItemIDsGui).ToList();
            List<int> disposedCollidableItemIDs = activeCollidableItemIDsGui.Except(activeCollidableItemIDs).ToList();

            disposeInactiveCollidableItems(disposedCollidableItemIDs);
            renderNewCollidableItems(gameState, newCollidableItemIDs);
            relocateExistingCollidableItems(gameState);
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

            for (int i = activeCollidableItemsGui.Count - 1; i >= 0; i--)
            {
                activeCollidableItemsGui[i].DisposeImage();
                Controls.Remove(activeCollidableItemsGui[i]);
                activeCollidableItemsGui.RemoveAt(i);
            }

            new GameOverPanel(this, (GameForm)Parent);
        }

        public void PauseGame()
        {
            pausedPanel.Show();
            pausedPanel.BringToFront();
        }

        public void ResumeGame()
        {
            pausedPanel.Hide();
            pausedPanel.SendToBack();
        }

        public void DisposeBackgroundImage()
        {
            Image backgroundImage = BackgroundImage;
            BackgroundImage = null;
            backgroundImage.Dispose();
        }

        private void relocateExistingCollidableItems(IGameStateUI gameState)
        {
            foreach (var collidableItemGui in activeCollidableItemsGui)
            {
                IGridItem? collidableItem = gameState.GetCollidableItemToDraw(collidableItemGui.ID);
                Debug.Assert(collidableItem != null);

                collidableItemGui.UpdateLocation(collidableItem.LocationX, collidableItem.LocationY);
            }
        }

        private void renderNewCollidableItems(IGameStateUI gameState, List<int> newCollidableItemIDs)
        {
            foreach (var id in newCollidableItemIDs)
            {
                IGridItem? collidableItem = gameState.GetCollidableItemToDraw(id);
                Debug.Assert(collidableItem != null);

                var newCollidableItemGui = new CollidableItemGui(
                    id, collidableItem.Width, collidableItem.Height, collidableItem.Image
                );
                activeCollidableItemsGui.Add(newCollidableItemGui);
                Controls.Add(newCollidableItemGui);
            }
        }

        private void disposeInactiveCollidableItems(List<int> disposedCollidableItemIDs)
        {
            for (int i = disposedCollidableItemIDs.Count - 1; i >= 0; i--)
            {
                int id = disposedCollidableItemIDs[i];
                CollidableItemGui? collidableItemGui = activeCollidableItemsGui.Find(laserBlast => laserBlast.ID == id);
                Debug.Assert(collidableItemGui != null);

                activeCollidableItemsGui.Remove(collidableItemGui);

                collidableItemGui.DisposeImage();
                Controls.Remove(collidableItemGui);
            }
        }

        public SpaceshipGui getSpaceshipGui(bool isHero) => isHero ? hero : enemy;
    }
}