namespace SpaceShooter.gui
{
    public partial class GameFrame : CustomFrame
    {
        public GameFrame(int gridDimensionX, int gridDimensionY)
        {
            InitializeComponent();
            Width = gridDimensionX;
            Height = gridDimensionY;
            FormClosed += AppClient.OnSubFrameClose;
        }
    }
}