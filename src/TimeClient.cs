using System.Reflection;
using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    public static class TimeClient
    {
        public delegate void RecurringAction();

        private const int gameTargetFPS = 65;

        private static readonly Timer gameUpdateTimer = new();
        private static readonly List<Timer> customActionTimers = new();

        static TimeClient()
        {
            gameUpdateTimer.Interval = (int)Math.Floor((decimal)(1000 / gameTargetFPS));
            DisableTime();
        }

        public static void EnableTime()
        {
            gameUpdateTimer.Enabled = true;
            foreach (Timer timer in customActionTimers)
                timer.Enabled = true;
        }

        public static void DisableTime()
        {
            gameUpdateTimer.Enabled = false;
            foreach (Timer timer in customActionTimers)
                timer.Enabled = false;
        }

        public static void AddGameUpdateAction(EventHandler gameUpdateAction)
        {
            gameUpdateTimer.Tick += gameUpdateAction;
        }

        public static void AddCustomRecurringAction(EventHandler customAction, int interval)
        {
            Timer? existingTimer = customActionTimers.Find(timer => timer.Interval == interval);
            if (existingTimer != null)
                existingTimer.Tick += customAction;
            else
            {
                Timer newTimer = new Timer()
                {
                    Interval = interval
                };
                newTimer.Tick += customAction;
                customActionTimers.Add(newTimer);
            }
        }

        public static void RemoveCustomRecurringAction(EventHandler customAction)
        {
            foreach (Timer timer in customActionTimers)
                timer.Tick -= customAction;
            removeUnusedCustomTimers();
        }

        private static void removeUnusedCustomTimers()
        {
            customActionTimers.RemoveAll(timer => getTimerTickInvocationListLength(timer) == 0);
        }

        private static int getTimerTickInvocationListLength(Timer timer)
        {
            var eventField = timer.GetType().GetField("Tick", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventDelegate = (Delegate)eventField.GetValue(timer);
            var invocatationList = eventDelegate.GetInvocationList();
            return invocatationList.Length;
        }
    }
}
