using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace SpaceShooter.gui
{
    public class StatsLabel : Label
    {
        private const float labelHeightRatio = 0.7f;
        private readonly string name;

        public StatsLabel(Control parent, string name, string initValue = "")
        {
            parent.Controls.Add(this);
            Parent = parent;

            this.name = name;

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            
            AutoSize = true;
            selectOptimalFontSize();
            TextAlign = ContentAlignment.MiddleCenter;
            UseCompatibleTextRendering = true;

            UpdateValue(initValue);
        }

        public void UpdateValue(string value) 
            => Text = $"{name}:  {value}";

        private void selectOptimalFontSize()
        {
            float fontSize = 1.0f;
            while (Height < labelHeightRatio * Parent.Height)
                Font = new Font(
                    "Microsoft Sans Serif",
                    fontSize++,
                    GraphicsUnit.Point
                );
        }
    }
}
