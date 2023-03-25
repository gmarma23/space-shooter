namespace SpaceShooter.gui
{
    public partial class HighscoresForm : CustomForm
    {
        private const int topEntriesCount = 10;

        public HighscoresForm()
        {
            InitializeComponent();
            setBackgroundImage();

            FormClosed += AppManager.OnSubFormClosed;

            new FormTitleLabel(this, "HIGHSCORES");

            List<(int, string)> highscores = DatabaseManager.GetTopEntries(topEntriesCount);
            
            while (highscores.Count < topEntriesCount)
                highscores.Add((0, "00:00:00"));

            List<string> nums = Enumerable.Range(1, topEntriesCount).Select(n => n.ToString()).ToList();
            List<string> scores = highscores.Select(item => item.Item1.ToString()).ToList();
            List<string> duration = highscores.Select(item => item.Item2.ToString()).ToList();

            var numsGroup = new LabelGroup(this, "No", nums)
            {
                Left = 0
            };

            var scoresGroup = new LabelGroup(this, "Score", scores)
            {
                Left = numsGroup.Width
            };

            var durationGroup = new LabelGroup(this, "Duration", duration)
            {
                Left = numsGroup.Width + scoresGroup.Width
            };
        }
    }
}
