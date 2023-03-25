namespace SpaceShooter.gui
{
    internal class MenuButton : Button
    {
        public MenuButton(Control parent) 
        {
            Parent = parent;

            BackColor = Color.FromArgb(60, Color.White);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Arial", 12.0F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.White;
            
            Width = Parent.Width;
            UseVisualStyleBackColor = false;

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
            BackColor = Color.FromArgb(60, Color.White);
        }
    }
}
