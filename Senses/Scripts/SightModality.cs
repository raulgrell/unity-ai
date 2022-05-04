using UnityEngine;

[CreateAssetMenu(menuName = "Sensor System/Modality/Sight")]
public class SightModality : Modality
{
    [SerializeField] private float viewAngle;

    public override bool ExtraTests(Signal signal, Transform emitter, Sensor sensor)
    {
        return TestSightCone(signal, emitter, sensor) && TestLineOfSight(signal, emitter, sensor);
    }

    private bool TestSightCone(Signal signal, Transform emitter, Sensor sensor)
    {
        var targetDirection = Vector3.Normalize(emitter.position - sensor.transform.position);
        var targetAngle = Vector3.SignedAngle(targetDirection, sensor.transform.forward, sensor.transform.up);
        return Mathf.Abs(targetAngle) < viewAngle;
    }

    private bool TestLineOfSight(Signal signal, Transform emitter, Sensor sensor)
    {
        var targetDirection = Vector3.Normalize(emitter.position - sensor.transform.position);
        var ray = new Ray(sensor.transform.position, targetDirection);
        if (!Physics.Raycast(ray, out RaycastHit hitInfo, maximumRange))
            return false;
        return hitInfo.transform == emitter;
    }
}