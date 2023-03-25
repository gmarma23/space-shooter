namespace SpaceShooter.gui
{
    public class StatsLabel : CustomLabel
    {
        private const float parentHeightRatio = 0.65f;
        private const float parentWidthRatio = 1;
        private readonly string name;

        public StatsLabel(Control parent, string name, string initValue = "") 
            : base(parent, initValue, parentHeightRatio, parentWidthRatio)
        {  
            this.name = name;
            UpdateValue(initValue);
        }

        public void UpdateValue(string value) 
            => Text = $"{name}:  {value}";
    }
}
