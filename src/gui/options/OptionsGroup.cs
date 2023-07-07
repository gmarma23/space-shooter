using SpaceShooter.resources;
using System.Diagnostics;
using System.Drawing;

namespace SpaceShooter.src.gui.options
{
    public abstract class OptionsGroup : GroupBox
    {
        protected const float widthRatio = 0.8f; 
        protected const float heightRatio = 0.165f;
        protected const float optionWidthRatio = 0.4f;
        protected const float optionHeightRatio = 0.5f;
        protected const float topMarginRatio = 0.35f;
        protected const float leftMarginRatio = 0.08f;

        public OptionsGroup(Control parent, string title) 
        {
            Parent = parent;
            Text = title;

            Width = (int)(Parent.ClientRectangle.Width * widthRatio);
            Height = (int)(Parent.ClientRectangle.Height * heightRatio);

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Font = new Font("Microsoft Sans Serif", 12.0f, FontStyle.Regular, GraphicsUnit.Point);

            Left = Parent.ClientRectangle.Width / 2 - Width / 2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Do not call the base OnPaint method to prevent painting the default border

            // Draw the text of the group box
            TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor, BackColor, TextFormatFlags.Left);

            // Draw a separator line below the text
            int textHeight = TextRenderer.MeasureText(Text, Font).Height;
            e.Graphics.DrawLine(Pens.White, ClientRectangle.Left, ClientRectangle.Top + textHeight + 4, ClientRectangle.Right, ClientRectangle.Top + textHeight + 4);
        }
    }
}
