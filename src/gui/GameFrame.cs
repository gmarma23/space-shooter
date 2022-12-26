using SpaceShooter.core;

namespace SpaceShooter.gui
{
    internal partial class GameFrame : CustomFrame
    {
        private HeroSpaceshipGui hero;
        private EnemySpaceshipGui enemy;

        public GameFrame(int gridDimensionX, int gridDimensionY, Dictionary<string, KeyEventHandler> keyEventHandlers)
        {
            InitializeComponent();
            Width = gridDimensionX;
            Height = gridDimensionY;

            FormClosed += AppClient.OnSubFrameClose;
            KeyDown += keyEventHandlers["OnKeyDown"];
            KeyUp += keyEventHandlers["OnKeyUp"];
        }

        public void RelocateSpaceship(GameState gameState, bool isHero)
        {
            int newX = 0, newY = 0;
            gameState.SpaceshipGetLocation(isHero, ref newX, ref newY);
            getSpaceship(isHero).UpdateLocation(newX, newY);
        }

        public void RenderHeroSpaceship(GameState gameState)
        {
            int width = 0, height = 0;
            gameState.SpaceshipGetSize(true, ref width, ref height);
            hero = new HeroSpaceshipGui(width, height);
            Controls.Add(hero);
        }

        public void RenderEnemySpaceship(GameState gameState)
        {
            int width = 0, height = 0;
            gameState.SpaceshipGetSize(false, ref width, ref height);
            EnemySpaceshipType enemyType = gameState.GetEnemySpaceshipType();
            enemy = new EnemySpaceshipGui(enemyType, width, height);
            Controls.Add(enemy);
        }

        public SpaceshipGui getSpaceship(bool isHero)
        {
            return isHero ? hero : enemy;
        }
    }
}