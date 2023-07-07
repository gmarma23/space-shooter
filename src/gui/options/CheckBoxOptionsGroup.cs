namespace SpaceShooter.src.gui.options
{
    public class CheckBoxOptionsGroup : OptionsGroup
    {
        private List<CheckBox> options;

        public CheckBoxOptionsGroup(Control parent, string title, List<(string, Action, Action)> optionDetails) : base(parent, title)
        {
            options = new List<CheckBox>();
            addOptions(optionDetails);
        }

        public Dictionary<string, bool> getSelected()
        {
            Dictionary<string, bool> selected = new Dictionary<string, bool>();

            foreach (var checkBox in Controls.OfType<CheckBox>())
                selected.Add(checkBox.Text, checkBox.Checked);

            return selected;
        }

        private void addOptions(List<(string, Action, Action)> optionDetails)
        {
            foreach ((string name, Action checkedAction, Action uncheckedAction) in optionDetails)
            {
                var newOption = new CustomCheckBox(this, name, checkedAction, uncheckedAction)
                {
                    Width = (int)(Width * optionWidthRatio),
                    Height = (int)(Height * optionHeightRatio),

                    Top = (int)(Height * topMarginRatio),
                    Left = (options.Count == 0 ? 0 : options.Last().Width) + (int)(Width * leftMarginRatio),
                };
                options.Add(newOption);
            }
        }
    }
}
