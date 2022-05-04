using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using UnityEngine;

public abstract class Modality : ScriptableObject
{
    [SerializeField] protected float maximumRange;
    [SerializeField] protected float attenuation;
    [SerializeField] protected float inverseTransmissionSpeed;

    public float Range => maximumRange;
    public float Attenuation => attenuation;
    public float InverseTransmissionSpeed => inverseTransmissionSpeed;

    public virtual bool ExtraTests(Signal signal, Transform emitter, Sensor sensor)
    {
        return true;
    }
}