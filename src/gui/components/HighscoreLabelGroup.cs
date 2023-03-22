namespace SpaceShooter.gui
{
    public class HighscoreLabelGroup : Panel
    {
        public HighscoreLabelGroup(Control parent, List<string> highscores)
        { 
            Parent = parent;

            foreach (var highscore in highscores)
            {
                Label highscoreLabel = new Label()
                {
                    BackColor = Color.Transparent,
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    AutoSize = true,
                    Font = new Font(
                        "Microsoft Sans Serif",
                        16.0F, GraphicsUnit.Point
                    ),
                    UseCompatibleTextRendering = true,
                };
                Controls.Add(highscoreLabel);
                highscoreLabel.Left = Parent.Width / 2 - Width / 2;
                highscoreLabel.Top = 0;
            };
        }   
        
    }
}
