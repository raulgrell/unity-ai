using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public SenseManager senseManager;
    public float speed = 10;
    public Signal presenceSignal;
    public Signal explosionSignal;
    
    void Update()
    {
        senseManager.AddSignal(presenceSignal, transform);

        if (Input.GetKeyDown(KeyCode.Space))
            senseManager.AddSignal(explosionSignal, transform);

        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * Time.deltaTime * speed;
    }
}
