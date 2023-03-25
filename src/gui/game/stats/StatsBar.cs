namespace SpaceShooter.gui
{
    public class StatsBar : Panel
    {
        private const int labelMargin = 10;

        public StatsLabel ScoreLabel { get; private init; }
        public StatsLabel ElapsedTimeLabel { get; private init; }

        public StatsBar(Control parent, int width, int height) 
        {
            Parent = parent;
            Width = width;
            Height = height;

            BackColor = Color.FromArgb(255, 40, 40, 40);

            ScoreLabel = new StatsLabel(this, "Score");
            ElapsedTimeLabel = new StatsLabel(this, "Elapsed Time", "00:00:00");

            ScoreLabel.Location = new Point(labelMargin, Height / 2 - ScoreLabel.Height / 2);
            ElapsedTimeLabel.Location = new Point(Width - ElapsedTimeLabel.Width - labelMargin, Height / 2 - ScoreLabel.Height / 2);
        }
    }
}
