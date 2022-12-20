using SpaceShooter.gui;

namespace SpaceShooter.gui
{
    public partial class MenuFrame : CustomFrame
    {
        public const double optionsYLocationRatio = 0.5;
        public const double optionsFrameWidthRatio = 0.35;
        public const double optionsFrameHeightRatio = 0.3;

        private Label title;
        private MenuOptions menuOptions;
        
        public MenuFrame()
        {
            InitializeComponent();
            menuOptions = new MenuOptions(this);
        }
    }
}
