using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    public class TimeClient
    {
        private Timer gameUpdateTimer;

        public delegate void RecurringAction();

        private Dictionary<RecurringAction, int> intervals;
        private Dictionary<RecurringAction, int> lastUpdateTimeSpan;

        public TimeClient(int gameTargetFPS)
        {
            gameUpdateTimer = new Timer();

            intervals = new Dictionary<RecurringAction, int>();
            lastUpdateTimeSpan = new Dictionary<RecurringAction, int>();

            gameUpdateTimer.Interval = 1000 / gameTargetFPS;
            gameUpdateTimer.Tick += onGameUpdate;
        }

        public void EnableTime() => gameUpdateTimer.Enabled = true;

        public void DisableTime() => gameUpdateTimer.Enabled = false;

        public void AddRecurringAction(RecurringAction action, int interval = 0)
        {
            if (gameUpdateTimer.Interval > interval)
                interval = gameUpdateTimer.Interval;

            intervals.Add(action, interval);
            lastUpdateTimeSpan.Add(action, 0);
        }

        public void RemoveRecurringAction(RecurringAction action)
        {
            intervals.Remove(action);
            lastUpdateTimeSpan.Remove(action);
        }

        private void onGameUpdate(object sender, EventArgs e)
        {
            foreach (RecurringAction action in intervals.Keys)
            {
                if (lastUpdateTimeSpan[action] < intervals[action])
                {
                    lastUpdateTimeSpan[action] += gameUpdateTimer.Interval;
                    continue;
                }

                action();
                lastUpdateTimeSpan[action] = 0;
            }
        }
    }
}
