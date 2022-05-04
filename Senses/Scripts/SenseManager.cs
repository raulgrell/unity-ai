using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class SenseManager : MonoBehaviour
{
    private readonly List<Sensor> sensors = new List<Sensor>();
    private readonly SimplePriorityQueue<Notification> notificationQueue = new SimplePriorityQueue<Notification>();

    void Start()
    {
        Sensor[] sensorsInScene = FindObjectsOfType<Sensor>();
        sensors.AddRange(sensorsInScene);
    }

    public void AddSignal(Signal signal, Transform emitter)
    {
        foreach (Sensor sensor in sensors)
        {
            if (!sensor.DetectsModality(signal.modality))
                continue;

            var distance = Vector3.Distance(emitter.position, sensor.transform.position);
            if (distance > signal.modality.Range)
                continue;

            var intensity = signal.strength * Mathf.Pow(signal.modality.Attenuation, distance);
            if (intensity < sensor.Threshold)
                continue;

            if (!signal.modality.ExtraTests(signal, emitter, sensor))
                continue;

            var time = Time.time + distance * signal.modality.InverseTransmissionSpeed;
            notificationQueue.Enqueue(new Notification(time, sensor, signal), time);
        }
    }

    void SendSignals()
    {
        while (notificationQueue.Count > 0)
        {
            var n = notificationQueue.First;
            if (Time.time > n.Time)
            {
                n.Sensor.Notify(n.Signal);
                notificationQueue.Dequeue();
            }
            else
            {
                break;
            }
        }
    }

    void Update()
    {
        SendSignals();
    }
}