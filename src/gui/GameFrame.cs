using SpaceShooter.core;
using System.Windows.Forms;

namespace SpaceShooter.gui
{
    internal partial class GameFrame : CustomFrame
    {
        private SpaceshipPanel hero;
        private SpaceshipPanel enemy;

        public GameFrame(int gridDimensionX, int gridDimensionY)
        {
            InitializeComponent();
            Width = gridDimensionX;
            Height = gridDimensionY;
            FormClosed += AppClient.OnSubFrameClose;
        }

        public void RelocateHeroSpaceship(GameState gameState)
        {
            int newX = 0, newY = 0;
            gameState.SpaceshipGetLocation(false, ref newX, ref newY);
            hero.Location = new Point(newX, newY);
        }

        public void RenderHeroSpaceship(GameState gameState)
        {
            int width = 0, height = 0;
            gameState.SpaceshipGetSize(false, ref width, ref height);
            hero = new SpaceshipPanel(SpaceshipType.Hero, width, height);
            Controls.Add(hero);
        }

        private ref SpaceshipPanel getSpaceshipRef(SpaceshipType type)
        {
            return ref (type == SpaceshipType.Hero) ? ref hero : ref enemy;
        }
    }
}