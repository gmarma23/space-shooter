using SpaceShooter.core;
using SpaceShooter.utils;

namespace SpaceShooter.gui
{
    public partial class GameForm : CustomForm
    {
        private const float statsPanelHeightRatio = 0.05f;

        public GameGridGui Grid { get; private init; }

        public StatsBar StatsBar { get; private init; }

        public GameForm(IGameStateUI gameState, EventHandler onGameResume)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.Manual;
            Location = ScreenUtils.GetLargestScreen().WorkingArea.Location;
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;

            int statsPanelWidth = gameState.Grid.DimensionX;
            int statsPanelHeight = (int)(statsPanelHeightRatio * gameState.Grid.DimensionY);

            int clientWidth = gameState.Grid.DimensionX;
            int clientHight = gameState.Grid.DimensionY + statsPanelHeight;
            ClientSize = new Size(clientWidth, clientHight);

            Grid = new GameGridGui(this, gameState, onGameResume)
            {
                Top = statsPanelHeight
            };

            StatsBar = new StatsBar(this, statsPanelWidth, statsPanelHeight);

            KeyPreview = true;
        }

        public static Size GetGameGridSize()
        {
            Size largestResolution = ScreenUtils.GetLargestScreen().Bounds.Size;
            int gridHeight = (int)((float)largestResolution.Height / (float)(1 + statsPanelHeightRatio));
            return new Size(largestResolution.Width, gridHeight);
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
