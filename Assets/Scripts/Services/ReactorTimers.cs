using System;
using UnityEngine;

public class ReactorTimers
{
    private static ReactorTimers instance;

    public static ReactorTimers Instance
    {
        get
        {
            if (instance == null) instance = new ReactorTimers();

            return instance;
        }
    }

    private ReactorTimers() {}

    public TimerHandle Start(float seconds, Action finished, MonoBehaviour owner) => Timer.Schedule(seconds, finished, owner);
}
