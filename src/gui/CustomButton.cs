using SpaceShooter.resources;
using SpaceShooter.utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace SpaceShooter.src.gui
{
    public class CustomButton : Button
    {
        private const float fontWidthRatio = 0.8f;
        private const float fontHeightRatio = 0.65f;

        private static readonly CachedSound menuButtonClickSoundFx = new(Resources.aud_menu_click);

        public CustomButton(Control parent, string text, float parentWidthRatio, float parentHeightRatio)
        {
            Parent = parent;
            Text = text;

            Width = (int)(Parent.Width * parentWidthRatio);
            Height = (int)(Parent.Height * parentHeightRatio);

            BackColor = Color.FromArgb(60, Color.White);
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            setFont();
            ForeColor = Color.White;

            UseVisualStyleBackColor = false;

            MouseEnter += onMouseEnter;
            MouseLeave += onMouseLeave;
            MouseDown += onMouseDown;
        }

        private void setFont()
        {
            const string fontFamily = "Microsoft Sans Serif";
            const GraphicsUnit graphicsUnit = GraphicsUnit.Point;
            float fontSize = 1.0f;

            Size textSize = new Size(0, 0);
            while (true)
            {
                if (textSize.Height > fontHeightRatio * Height ||
                    textSize.Width > fontWidthRatio * Width)
                    break;

                Font = new Font(fontFamily, fontSize++, FontStyle.Regular, graphicsUnit);
                textSize = TextRenderer.MeasureText(Text, Font);
            }
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

        private void onMouseDown(object? sender, EventArgs e)
            => AudioPlayer.Player.PlaySound(menuButtonClickSoundFx);
    }
}
