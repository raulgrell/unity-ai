using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification
{
    private readonly float time;
    private readonly Sensor sensor;
    private readonly Signal signal;

    public Notification(float time, Sensor sensor, Signal signal)
    {
        this.time = time;
        this.sensor = sensor;
        this.signal = signal;
    }

    public float Time => time;
    public Signal Signal => signal;
    public Sensor Sensor => sensor;
}
