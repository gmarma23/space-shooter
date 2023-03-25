using SpaceShooter.resources;
using SpaceShooter.utils;

namespace SpaceShooter.gui
{
    public class MenuButton : Button
    {
        private static readonly CachedSound menuButtonClickSoundFx = new(Resources.aud_menu_click);

        public MenuButton(Control parent) 
        {
            Parent = parent;

            BackColor = Color.FromArgb(60, Color.White);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.White;
            
            Width = Parent.Width;
            UseVisualStyleBackColor = false;

            MouseEnter += onMouseEnter;
            MouseLeave += onMouseLeave;
            Click += onMouseClick;
        }

        private void onMouseEnter(object? sender, EventArgs e)
        {
            UseVisualStyleBackColor = false;
            FlatAppearance.MouseOverBackColor = Color.FromArgb(120, Color.White);
        }

        private void onMouseLeave(object? sender, EventArgs e)
        {
            UseVisualStyleBackColor = true;
            BackColor = Color.FromArgb(60, Color.White);
        }

        private void onMouseClick(object? sender, EventArgs e) 
            => AudioPlayer.Player.PlaySound(menuButtonClickSoundFx);
    }
}
