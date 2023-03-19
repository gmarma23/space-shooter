namespace SpaceShooter.gui
{
    public partial class MenuForm : CustomForm
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
        
        public MenuForm()
        {
            InitializeComponent();

            setTitleLabel();
            setHeroPicBox();
            _ = new MenuOptions(this);
        }

        private void setTitleLabel()
        {
            Label title = new Label();
            Controls.Add(title);
            title.BackColor = Color.Transparent;
            title.Font = new Font(
                "Microsoft Sans Serif", 
                25.8F, FontStyle.Bold | FontStyle.Italic, 
                GraphicsUnit.Point
            );
            title.ForeColor = Color.White;
            title.Width = (int)(titleWidthRatio * ClientRectangle.Width);
            title.Height = (int)(titleHeightRatio * ClientRectangle.Height);
            title.Text = "SPACE SHOOTER";
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.UseCompatibleTextRendering = true;
            title.Location = new Point(
                ClientRectangle.Width / 2 - title.Width / 2,
                (int)(ClientRectangle.Height * titleLocationYRatio)
            );
        }

        private void setHeroPicBox()
        {
            int heroPicBoxWidth = (int)(ClientRectangle.Width * heroPicBoxWidthRatio);
            int heroPicBoxHeight = (int)(ClientRectangle.Height * heroPicBoxHeightRatio);
            PictureBox heroPicBox = new PictureBox()
            {
                Width = heroPicBoxWidth,
                Height = heroPicBoxHeight,
                Image = resources.Resources.img_hero_spaceship,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            Controls.Add(heroPicBox);

            int heroPicBoxLocationX = ClientRectangle.Width / 2 - heroPicBox.Width / 2;
            int heroPicBoxLocationY = (int)(ClientRectangle.Height * heroPicBoxLocationYRatio);
            heroPicBox.Location = new Point(heroPicBoxLocationX, heroPicBoxLocationY);
        }
    }
}
