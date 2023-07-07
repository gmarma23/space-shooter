using static SpaceShooter.utils.CustomExceptions;

namespace SpaceShooter.src.gui.options
{
    public class CustomCheckBox : CheckBox
    {
        public CustomCheckBox(Control parent, string text, Action checkedAction, Action uncheckedAction) 
        {
            Parent = parent;
            Text = text;

            setChecked();
            CheckedChanged += (sender, e) => onCheckedChenged(checkedAction, uncheckedAction);

            Parent.Controls.Add(this);
        }

        private void setChecked()
        {
            try 
            { 
                Checked = DatabaseManager.GetOptionValue(Text);
            }
            catch (EntryNotFoundException)
            {
                Checked = true;
            }
        }

        private void onCheckedChenged(Action checkedAction, Action uncheckedAction)
        {
            if (Checked)
                checkedAction();
            else 
                uncheckedAction();
        }
    }
}
