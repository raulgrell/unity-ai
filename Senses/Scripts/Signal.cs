using UnityEngine;

[CreateAssetMenu(menuName = "Sensor System/Signal")]
public class Signal : ScriptableObject
{
    public float strength;
    public Modality modality;
}