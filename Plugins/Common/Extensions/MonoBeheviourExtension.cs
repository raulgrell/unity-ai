using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    /// <summary>
    /// Disable the specified behaviour if the assertion is false, and throw a warning
    /// </summary>
    public static void Assert(this MonoBehaviour behaviour, bool assertValue, string message = "")
    {
        if (!assertValue)
        {
            Debug.LogWarning("Assert failed. " + message);
            behaviour.enabled = false;
        }
    }
}
