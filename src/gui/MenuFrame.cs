using SpaceShooter.gui;

namespace SpaceShooter.gui
{
    public partial class MenuFrame : CustomFrame
    {
        public const double titleWidthRatio = 0.8;
        public const double titleHeightRatio = 0.3;
        public const double titleYLocationRatio = 0.02;

        public const double optionsYLocationRatio = 0.77;
        public const double optionsWidthRatio = 0.35;
        public const double optionsHeightRatio = 0.3;

        private Label title;
        private MenuOptions menuOptions;
        
        public MenuFrame()
        {
            InitializeComponent();

            title = new Label();
            setTitleLabel();

            menuOptions = new MenuOptions(this);
        }

        private void setTitleLabel()
        {
            Controls.Add(title);
            title.BackColor = Color.Transparent;
            title.Font = new Font(
                "Microsoft Sans Serif", 
                25.8F, FontStyle.Bold | FontStyle.Italic, 
                GraphicsUnit.Point);
            title.ForeColor = Color.White;
            title.Width = (int)(titleWidthRatio * ClientRectangle.Width);
            title.Height = (int)(titleHeightRatio * ClientRectangle.Height);
            title.Text = "SPACE SHOOTER";
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.UseCompatibleTextRendering = true;
            title.Location = new Point(
                ClientRectangle.Width / 2 - title.Width / 2,
                (int)(ClientRectangle.Height * titleYLocationRatio));
        }
    }
}
