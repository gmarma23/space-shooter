using System.Diagnostics;
using System.Reflection;
using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    public class TimeManager
    {
        private readonly Timer gameUpdateTimer;
        private readonly List<Timer> customActionTimers;
        private readonly Stopwatch stopwatch;

        public static double DeltaTime { get; private set; }
        public static double ElapsedGameTime { get; private set; }

        public bool IsTimeActive;

        public TimeManager(int gameTargetFPS)
        {
            gameUpdateTimer = new Timer();
            customActionTimers = new List<Timer>();
            stopwatch = new Stopwatch();

            gameUpdateTimer.Interval = (int)Math.Floor((decimal)(1000 / gameTargetFPS));
            ElapsedGameTime = 0;
            DeltaTime = 0;
            
            DisableTime();
        }

        public void EnableTime()
        {
            gameUpdateTimer.Enabled = true;

            foreach (Timer timer in customActionTimers)
                timer.Enabled = true;

            stopwatch.Start();
            IsTimeActive = true;
        }

        public void DisableTime()
        {
            gameUpdateTimer.Enabled = false;

            foreach (Timer timer in customActionTimers)
                timer.Enabled = false;

            stopwatch.Stop();
            IsTimeActive = false;
        }

        public void UpdateDeltaTime()
        {
            DeltaTime = stopwatch.Elapsed.TotalSeconds;
            ElapsedGameTime += DeltaTime;
            stopwatch.Restart();
        }

        public void AddMainRecurringAction(EventHandler gameUpdateAction)
            => gameUpdateTimer.Tick += gameUpdateAction;

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
            => customActionTimers.RemoveAll(timer => getTimerTickInvocationListLength(timer) == 0);

        private int getTimerTickInvocationListLength(Timer timer)
        {
            var eventField = timer.GetType().GetField("Tick", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventDelegate = (Delegate)eventField.GetValue(timer);
            var invocatationList = eventDelegate.GetInvocationList();
            return invocatationList.Length;
        }
    }
}
