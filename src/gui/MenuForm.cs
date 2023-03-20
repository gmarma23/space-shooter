namespace SpaceShooter.gui
{
    public partial class MenuForm : CustomForm
    {
        private const float heroPicBoxLocationYRatio = 0.335f;
        private const float heroPicBoxWidthRatio = 0.23f;
        private const float heroPicBoxHeightRatio = 0.2f;
        
        public MenuForm()
        {
            InitializeComponent();

            new FormTitleLabel(this) 
            {
                Text = "SPACE SHOOTER"
            };

            setHeroPicBox();
            _ = new MenuOptions(this);
        }

        private void setHeroPicBox()
        {
            int heroPicBoxWidth = (int)(ClientRectangle.Width * heroPicBoxWidthRatio);
            int heroPicBoxHeight = (int)(ClientRectangle.Height * heroPicBoxHeightRatio);
            PictureBox heroPicBox = new()
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
