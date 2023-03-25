namespace SpaceShooter.gui
{
    public class CustomLabel : Label
    {
        public CustomLabel(Control parent, string text, float parentHeightRatio, float parentWidthRatio, FontStyle fontStyle = 0)
        {
            Parent = parent;
            Parent.Controls.Add(this);
            BackColor = Color.Transparent;
            AutoSize = true;
            Text = text;
            
            setFont(parentHeightRatio, parentWidthRatio, fontStyle);
            ForeColor = Color.White;
            TextAlign = ContentAlignment.MiddleCenter;
            UseCompatibleTextRendering = true;

            Left = Parent.ClientRectangle.Width / 2 - Width / 2;
        }

        private void setFont(float parentHeightRatio, float parentWidthRatio, FontStyle fontStyle)
        {
            const string fontFamily = "Microsoft Sans Serif";
            const GraphicsUnit graphicsUnit = GraphicsUnit.Point;
            float fontSize = 1.0f;

            while (Height < parentHeightRatio * Parent.ClientRectangle.Height)
            {
                if (Width > parentWidthRatio * Parent.ClientRectangle.Width) 
                    break;
                Font = new Font(fontFamily, fontSize++, fontStyle, graphicsUnit);
            }
        }
    }
}
