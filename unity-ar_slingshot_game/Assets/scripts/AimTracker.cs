using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimTracker : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera mainCam;

    // aim rainge to increment with pingpong
    private float _zIncrement = 1;
    private Vector3 _camPos;
    //private bool _incrementing = true;

    // Update is called once per frame
    void Update()
    {
        // update camera position
        _camPos = mainCam.transform.forward;
        // set the position of the first point of the line
        lineRenderer.SetPosition(0, _camPos );
        // create the aim position and add the increment for distance
        Vector3 aimPos = _camPos;
        aimPos.z += _zIncrement;
        // set the position of the second point of the line
        lineRenderer.SetPosition(1, aimPos);
        // ping pong the z increment
        _zIncrement = Mathf.PingPong(Time.deltaTime, 10f);

    }
}
