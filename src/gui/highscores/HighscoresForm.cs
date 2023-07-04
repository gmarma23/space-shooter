using SpaceShooter.src.gui;

namespace SpaceShooter.gui
{
    public partial class HighscoresForm : CustomForm
    {
        private const int topEntriesCount = 10;
        private const float okBtnHeightRatio = 0.05f;
        private const float okBtnWidthRatio = 0.18f;
        private const float okBtnMarginRatio = 0.05f;

        public HighscoresForm()
        {
            InitializeComponent();
            setBackgroundImage();

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

            var okBtn = new CustomButton(this)
            {
                Width = (int)(Width * okBtnWidthRatio),
                Height = (int)(Height * okBtnHeightRatio),
                Top = numsGroup.Top + numsGroup.Height + (int)(Height * okBtnMarginRatio),
                Text = "OK"
            };
            okBtn.Left = ClientRectangle.Width / 2 - okBtn.Width / 2;
            okBtn.Click += (sender, e) => Close();
        }
    }
}
