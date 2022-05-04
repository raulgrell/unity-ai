using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rand : MonoBehaviour
{
    private void Start()
    {
        Texture2D tex = new Texture2D(256, 256);
        GetComponent<Renderer>().material.mainTexture = tex;
        
        for (int i = 0; i < 1024; i++)
        {
            int randX = (int)NextGaussian(128, 20, 0, 256);
            int randY = (int)NextGaussian(128, 20, 0, 256);
            var color = Random.ColorHSV(0, 1, 0, 1);
            
            tex.SetPixel(randX, randY, color);
        }
        
        tex.Apply();
    }
    
    public static float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        }
        while (s >= 1.0f ||  s == 0f);
        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);
        return v1 * s;
    }

    public static float NextGaussian(float mean, float std_dev)
    {
        return mean + NextGaussian() * std_dev;
    }
    
    public static float NextGaussian(float mean, float std_dev, float min, float max)
    {
        float v;
        do
        {
            v = NextGaussian(mean, std_dev);
        }
        while (v < min || v > max);
        return v;
    }
}