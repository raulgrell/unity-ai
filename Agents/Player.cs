using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private float speed = 8;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var dx = Input.GetAxis("Horizontal") * speed;
        var dy = Input.GetAxis("Vertical") * speed;
        
        controller.SimpleMove(new Vector3(dx, 0, dy));
    }
}
