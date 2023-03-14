namespace SpaceShooter.gui
{
    internal class MenuButton : Button
    {
        public MenuButton(MenuOptions menuOptionsPanel, string text, EventHandler onClick) 
        {
            BackColor = Color.FromArgb(80, Color.White);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Arial", 12.0F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.White;
            Parent = menuOptionsPanel;
            Width = Parent.Width;
            Height = menuOptionsPanel.MenuButtonHeight;
            UseVisualStyleBackColor = false;
            Text = text;

            Click += onClick;
            MouseEnter += onMouseEnter;
            MouseLeave += onMouseLeave;
        }

        private void onMouseEnter(object sender, EventArgs e)
        {
            UseVisualStyleBackColor = false;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(120, Color.White);
        }

        private void onMouseLeave(object sender, EventArgs e)
        {
            UseVisualStyleBackColor = true;
            BackColor = Color.FromArgb(80, Color.White);
        }
    }
}
