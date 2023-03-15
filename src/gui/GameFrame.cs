using SpaceShooter.core;
using System.Diagnostics;

namespace SpaceShooter.gui
{
    public partial class GameFrame : CustomFrame
    {
        public GameGridGui Grid { get; private init; }

        public GameFrame(
            IGameStateUI gameState,
            Dictionary<string, KeyEventHandler> keyEventHandlers,
            FormClosedEventHandler onGameFrameClosed
        )
        {
            InitializeComponent();

            ClientSize = new Size(
                gameState.Grid.DimensionX,
                gameState.Grid.DimensionY 
            );

            FormClosed += AppManager.OnSubFrameClosed;
            FormClosed += onGameFrameClosed;
            KeyDown += keyEventHandlers["OnKeyDown"];
            KeyUp += keyEventHandlers["OnKeyUp"];

            Grid = new GameGridGui(this, gameState);
        }

       
    }
}
