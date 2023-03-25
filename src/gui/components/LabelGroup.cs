using SpaceShooter.src.gui.components;

namespace SpaceShooter.gui
{
    public class LabelGroup : Panel
    {
        private const float parentLocationYRatio = 0.2f;
        private const float parentHeightRatio = 0.7f;
        private const float parentWidthRatio = 0.33f;
        private const float labelMarginRatio = 0.33f;

        public LabelGroup(Control parent, string title, List<string> highscores)
        { 
            Parent = parent;
            Parent.Controls.Add(this);

            Height = (int)(Parent.ClientRectangle.Height * parentHeightRatio);
            Width = (int)(Parent.ClientRectangle.Width * parentWidthRatio);
            Top = (int)(Parent.ClientRectangle.Height * parentLocationYRatio);

            LabelGroupTitleLabel groupTitleLabel = new LabelGroupTitleLabel(this, title);
            int initY = groupTitleLabel.Top + groupTitleLabel.Height + 20;

            float labelHeight = (Height - initY) / ((1 + labelMarginRatio) * (highscores.Count + 1));
            float labelHeightRatio = labelHeight / Height; 
            int margin = (int)(Height * labelHeightRatio * labelMarginRatio);

            for (int i = 0; i < highscores.Count; i++)
            {
                HighscoreLabel highscoreLabel = new HighscoreLabel(this, highscores[i], labelHeightRatio);
                highscoreLabel.Top = initY + i * (highscoreLabel.Height + margin);
            }
        }   
    }
}
