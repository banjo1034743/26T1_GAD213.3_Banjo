using System;
using UnityEngine;

[Serializable]
public struct Timer
{
    public float Duration;
    public bool Repeats;

    private float nextTriggerTime;

    /// <summary>
    /// Indicates whether the timer has expired and is ready to trigger.
    /// </summary>
    public bool HasExpired
    {
        get
        {
            if (Time.time < nextTriggerTime)
            {
                return false;
            }
            else
            {
                if (Repeats)
                    Restart();

                return true;
            }
        }
    }

    /// <summary>
    /// Starts or restarts the timer.
    /// </summary>
    public void Restart()
    {
        nextTriggerTime = Time.time + Duration;
    }
}
