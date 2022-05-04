using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WanderingUnit : MonoBehaviour
{
    public float speed = 2;
    public float rotationSpeed = 1.5f;
    public float seed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        seed = Random.Range(-1, 1);
        transform.Rotate(0, Random.Range(0, 360), 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        
        rotationSpeed = Mathf.PerlinNoise((Time.time + seed) * seed, 0);
        rotationSpeed *= 20;
        rotationSpeed -= 10;
        
        transform.Rotate(0, rotationSpeed, 0);
        transform.position = pos + transform.forward * speed * Time.deltaTime;
    }
}
