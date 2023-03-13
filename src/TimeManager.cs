using System.Reflection;
using Timer = System.Windows.Forms.Timer;

namespace SpaceShooter
{
    public class TimeManager
    {
        private readonly Timer gameUpdateTimer;
        private readonly List<Timer> customActionTimers;

        private static int gameCycles;

        public static int GameUpdateRate { get; private set; }
        public static int ElapsedGameTime { get => gameCycles * GameUpdateRate; }

        public TimeManager(int gameTargetFPS = 65)
        {
            gameUpdateTimer = new Timer();
            customActionTimers = new List<Timer>();

            gameUpdateTimer.Interval = (int)Math.Floor((decimal)(1000 / gameTargetFPS));
            GameUpdateRate = gameUpdateTimer.Interval;
            gameCycles = 0;
            AddMainRecurringAction(IncreaseGameCycles);
            
            DisableTime();
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

        private void IncreaseGameCycles(object? sender, EventArgs e) => gameCycles++;

        private int getTimerTickInvocationListLength(Timer timer)
        {
            var eventField = timer.GetType().GetField("Tick", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventDelegate = (Delegate)eventField.GetValue(timer);
            var invocatationList = eventDelegate.GetInvocationList();
            return invocatationList.Length;
        }
    }
}
