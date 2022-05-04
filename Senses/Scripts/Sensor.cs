using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] private float threshold;
    [SerializeField] private Modality[] modalities;

    public float Threshold => threshold;

    public bool DetectsModality(Modality modality)
    {
        foreach (Modality m in modalities)
        {
            if (m.GetType().IsInstanceOfType(modality))
                return true;
        }

        return false;
    }

    public bool Notify(Signal signal)
    {
        if (signal.modality is HearingModality)
            Debug.Log($"{name} heard something");
        else if (signal.modality is SightModality)
            Debug.Log($"{name} saw something");
        else if (signal.modality is TouchModality)
            Debug.Log($"{name} felt something");
        else
        {
            Debug.Log($"Modality {signal.modality.name} not available");
        }

        return true;
    }
}