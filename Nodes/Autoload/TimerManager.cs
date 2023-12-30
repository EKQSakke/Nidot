﻿namespace Nidot;

public partial class TimerManager : Node
{
    class TimedEvent
    {
        public double Time
        {
            get { return Time; }
            set
            {
                CurrentTime = value;
            }
        }

        public double CurrentTime { get; private set; }
        public event Action OnComplete;

        public bool ReduceTime(double delta)
        {
            CurrentTime -= delta;

            if (CurrentTime <= 0)
            {
                OnComplete?.Invoke();
                return true;
            }

            return false;
        }
    }

    Dictionary<string, TimedEvent> timers = new();

    /// <summary>
    /// Adds a timer to the manager. If the key already exists, the timer will be reset.
    /// </summary>
    /// <note>Does not support multiple timers with same key</note>
    public void AddTimer(string key, double time, Action onCompleteAction)
    {
        if (timers.ContainsKey(key))
        {
            var timer = timers[key];
            timer.Time = time;
            return;
        }

        var newTimer =new TimedEvent()
        {
            Time = time,
        };
        newTimer.OnComplete += onCompleteAction;
        timers.Add(key, newTimer);
    }

    public void RemoveTimer(string key)
    {
        if (timers.ContainsKey(key))
        {
            timers.Remove(key);
        }
    }

    public override void _Process(double delta)
    {
        foreach (var item in timers)
        {
            if (item.Value.ReduceTime(delta))
            {
                timers.Remove(item.Key);
            }
        }
    }
}