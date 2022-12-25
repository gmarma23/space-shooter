namespace SpaceShooter.gui
{
    public partial class MenuFrame : CustomFrame
    {
        public const double optionsLocationYRatio = 0.77;
        public const double optionsWidthRatio = 0.35;
        public const double optionsHeightRatio = 0.3;

        private const double titleWidthRatio = 0.8;
        private const double titleHeightRatio = 0.3;
        private const double titleLocationYRatio = 0.02;

        private const double heroPicBoxWidthRatio = 0.23;
        private const double heroPicBoxHeightRatio = 0.2;
        private const double heroPicBoxLocationYRatio = 0.335;

        private Label title;
        private SpaceshipPictureBox heroPicBox;
        private MenuOptions menuOptions;
        
        public MenuFrame()
        {
            InitializeComponent();

            setTitleLabel();
            setHeroPicBox();
            menuOptions = new MenuOptions(this);
        }

        private void setTitleLabel()
        {
            title = new Label();
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
                (int)(ClientRectangle.Height * titleLocationYRatio));
        }

        private void setHeroPicBox()
        {
            int heroPicBoxWidth = (int)(ClientRectangle.Width * heroPicBoxWidthRatio);
            int heroPicBoxHeight = (int)(ClientRectangle.Height * heroPicBoxHeightRatio);
            heroPicBox = new SpaceshipPictureBox(SpaceshipType.Hero, heroPicBoxWidth, heroPicBoxHeight);
            Controls.Add(heroPicBox);
            heroPicBox.Location = new Point(
                ClientRectangle.Width / 2 - heroPicBox.Width / 2,
                (int)(ClientRectangle.Height * heroPicBoxLocationYRatio));
        }
    }
}
