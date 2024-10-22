using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float rotationSpeed;
    public bool moveCam = true;

    void Start()
    {
        // lock cursor in center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (moveCam)
        {
            transform.position = player.transform.position + offset;

            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);

            offset = camTurnAngle * offset;
            transform.LookAt(player.transform);
        }
    }

}
