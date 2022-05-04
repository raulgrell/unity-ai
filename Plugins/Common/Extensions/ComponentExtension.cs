using UnityEngine;
using System.Collections.Generic;

public static class ComponentExtension
{
    public static T EnsureComponent<T>(this Component value) where T : Component
    {
        T result = value.GetComponent<T>();

        if (result == null)
            result = value.gameObject.AddComponent<T>();

        return result;
    }

    public static bool HasComponent<T>(this GameObject gameObject) where T : Component
    {
        return (gameObject.GetComponent<T>() != null);
    }

    public static T[] GetComponentsInChildrenWithTag<T>(this GameObject gameObject, string tag)
        where T : Component
    {
        List<T> results = new List<T>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject childObject = gameObject.transform.GetChild(i).gameObject;
            if (childObject.CompareTag(tag))
                if (childObject.HasComponent<T>())
                    results.Add(childObject.GetComponent<T>());
        }

        return results.ToArray();
    }
}

