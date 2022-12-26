using SpaceShooter.core;

namespace SpaceShooter.gui
{
    internal partial class GameFrame : CustomFrame
    {
        private HeroSpaceshipGui hero;
        private EnemySpaceshipGui enemy;
        private List<LaserBlastPictureBox> activeLaserBlasts;

        public GameFrame(int gridDimensionX, int gridDimensionY, Dictionary<string, KeyEventHandler> keyEventHandlers)
        {
            InitializeComponent();
            Width = gridDimensionX;
            Height = gridDimensionY;

            activeLaserBlasts = new List<LaserBlastPictureBox>();

            FormClosed += AppClient.OnSubFrameClose;
            KeyDown += keyEventHandlers["OnKeyDown"];
            KeyUp += keyEventHandlers["OnKeyUp"];
        }

        public void RelocateSpaceship(IDrawGameStateUI gameState, bool isHero)
        {
            (int x, int y) = gameState.GetSpaceshipLocation(isHero);
            getSpaceship(isHero).UpdateLocation(x, y);
        }

        public void RenderHeroSpaceship(IDrawGameStateUI gameState)
        {
            (int width, int height) = gameState.GetSpaceshipSize(true);
            hero = new HeroSpaceshipGui(width, height);
            Controls.Add(hero);
        }

        public void RenderEnemySpaceship(IDrawGameStateUI gameState)
        {
            (int width, int height) = gameState.GetSpaceshipSize(false);
            EnemySpaceshipType enemyType = gameState.GetEnemySpaceshipType();
            enemy = new EnemySpaceshipGui(enemyType, width, height);
            Controls.Add(enemy);
        }

        public void RenderLaserBlast(IDrawGameStateUI gameState, int laserBlastNumCode)
        {
            bool isHero = gameState.IsHeroLaserBlast(laserBlastNumCode);
            (int width, int height) = gameState.GetLaserBlastSize(laserBlastNumCode);
            LaserBlastPictureBox newLaserBlast = new LaserBlastPictureBox(isHero, laserBlastNumCode, width, height);
            activeLaserBlasts.Add(newLaserBlast);
            Controls.Add(newLaserBlast);
        }

        public void RelocateLaserBlasts(IDrawGameStateUI gameState)
        {
            foreach(LaserBlastPictureBox laserBlast in activeLaserBlasts)
            {
                (int x, int y) = gameState.GetLaserBlastLocation(laserBlast.NumCode);
                laserBlast.UpdateLocation(x, y);
            }
        }

        public SpaceshipGui getSpaceship(bool isHero)
        {
            return isHero ? hero : enemy;
        }
    }
}