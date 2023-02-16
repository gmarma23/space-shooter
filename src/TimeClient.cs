using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    public static class TimeClient
    {
        public delegate void RecurringAction();

        private const int gameTargetFPS = 60;

        private static readonly Timer gameUpdateTimer = new();
        private static readonly Dictionary<RecurringAction, int> intervals = new();
        private static readonly Dictionary<RecurringAction, int> lastUpdateTimeSpan = new();

        static TimeClient()
        {
            gameUpdateTimer.Interval = (int)Math.Ceiling((decimal)(1000 / gameTargetFPS));
            gameUpdateTimer.Tick += onGameUpdate;
            DisableTime();
        }

        public static void EnableTime() => gameUpdateTimer.Enabled = true;

        public static void DisableTime() => gameUpdateTimer.Enabled = false;

        public static void AddRecurringAction(RecurringAction action, int interval = 0)
        {
            if (gameUpdateTimer.Interval > interval)
                interval = gameUpdateTimer.Interval;

            intervals.Add(action, interval);
            lastUpdateTimeSpan.Add(action, 0);
        }

        public static void RemoveRecurringAction(RecurringAction action)
        {
            intervals.Remove(action);
            lastUpdateTimeSpan.Remove(action);
        }

        private static void onGameUpdate(object? sender, EventArgs e)
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
