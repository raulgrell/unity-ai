using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public abstract class Steering : MonoBehaviour
{
    [SerializeField] private float weight = 1f;
    
    public float Weight => weight;

    public abstract SteeringData GetSteering(SteeringBehaviour sb);
}