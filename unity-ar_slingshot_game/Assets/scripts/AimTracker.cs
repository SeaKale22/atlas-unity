using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AimTracker : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera mainCam;
    public float yCamPosOffSet = -.1f;
    public float maxLineLength = 5f;
    // aim range to increment with pingpong
    public float _zIncrement = 2;
    
    private Vector3 _camPos;
    private float _selectedPlaneY;
    //private bool _incrementing = true;

    // Update is called once per frame
    void Update()
    {
        // update camera position
        _camPos = mainCam.transform.position;
        // create the aim position
        Vector3 aimPos = mainCam.transform.forward * _zIncrement;
        aimPos.y = _selectedPlaneY;
        // aimPos.x = mainCam.transform.forward.x;
        // add the y offset to the camPos
        _camPos.y += yCamPosOffSet;
        // set the position of the first point of the line
        lineRenderer.SetPosition(0, _camPos );
        // add the zIncrement to aimPos for distance
        // aimPos.z += _zIncrement;
        // set the position of the second point of the line
        lineRenderer.SetPosition(1, aimPos);
        // ping pong the z increment
        _zIncrement = Mathf.PingPong(Time.time, 2.5f);
        _zIncrement *= 2;

    }
    public void SelectedPlaneYSetter(float value)
    {
        _selectedPlaneY = value;
    }

    public Vector3 EndPosGetter()
    {
        return lineRenderer.GetPosition(1);
    }
}
