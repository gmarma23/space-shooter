using SpaceShooter.core;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace SpaceShooter.gui
{
    public partial class GameFrame : CustomFrame
    {
        private const float statsPanelHeightRatio = 0.05f;

        public GameGridGui Grid { get; private init; }

        public StatsBar StatsPanel { get; private init; }

        public GameFrame(
            IGameStateUI gameState,
            Dictionary<string, KeyEventHandler> keyEventHandlers,
            FormClosedEventHandler onGameFrameClosed
        )
        {
            InitializeComponent();

            int statsPanelWidth = gameState.Grid.DimensionX;
            int statsPanelHeight = (int)(statsPanelHeightRatio * gameState.Grid.DimensionY);

            int clientWidth = gameState.Grid.DimensionX;
            int clientHight = gameState.Grid.DimensionY + statsPanelHeight;
            ClientSize = new Size(clientWidth, clientHight);

            FormClosed += AppManager.OnSubFrameClosed;
            FormClosed += onGameFrameClosed;
            KeyDown += keyEventHandlers["OnKeyDown"];
            KeyUp += keyEventHandlers["OnKeyUp"];

            Grid = new GameGridGui(this, gameState)
            {
                Top = statsPanelHeight
            };

            StatsPanel = new StatsBar(this, statsPanelWidth, statsPanelHeight);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Double-buffer a window along with all of its child controls.
                // Everything gets rendered into an off-screen buffer.

                CreateParams cp = base.CreateParams;
                // Turn on WS_EX_COMPOSITED
                cp.ExStyle |= 0x02000000;  
                return cp;
            }
        }
    }
}
