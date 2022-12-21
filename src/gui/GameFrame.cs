namespace SpaceShooter.gui
{
    public partial class GameFrame : CustomFrame
    {
        public GameFrame(int gridXDimension, int gridYDimension)
        {
            InitializeComponent();
            Width = gridXDimension;
            Height = gridYDimension;
            FormClosed += AppClient.OnSubFrameClose;
        }
    }
}