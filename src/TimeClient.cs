using System.Reflection;

using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    internal class TimeClient
    {
        private Timer gameUpdateTimer;
        private List<Timer> customActionTimers;

        public TimeClient(int gameTargetFPS)
        {
            gameUpdateTimer = new Timer();
            customActionTimers = new List<Timer>();

            gameUpdateTimer.Interval = 1000 / gameTargetFPS;
        }

        public void EnableTime()
        {
            gameUpdateTimer.Enabled = true;
            foreach (Timer timer in customActionTimers)
                timer.Enabled = true;
        }

        public void DisableTime() 
        {
            gameUpdateTimer.Enabled = false;
            foreach (Timer timer in customActionTimers)
                timer.Enabled = false;
        }

        public void AddGameUpdateAction(EventHandler gameUpdateAction)
        {
            gameUpdateTimer.Tick += gameUpdateAction;
        }

        public void AddCustomRecurringAction(EventHandler customAction, int interval)
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

        public void RemoveCustomRecurringAction(EventHandler customAction)
        {
            foreach (Timer timer in customActionTimers)
                timer.Tick -= customAction;
            removeUnusedCustomTimers();
        }

        private void removeUnusedCustomTimers()
        {
            customActionTimers.RemoveAll(timer => getTimerTickInvocationListLength(timer) == 0);
        }

        private int getTimerTickInvocationListLength(Timer timer)
        {
            var eventField = timer.GetType().GetField("Tick", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventDelegate = (Delegate)eventField.GetValue(timer);
            var invocationList = eventDelegate.GetInvocationList();
            return invocationList.Length;
        }
    }
}
