using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSelectionManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    // start button canvas
    public GameObject StartCanvas;
    // selected plane, default null
    private ARPlane _selectedPlane = null;
    
    // Update is called once per frame
    void Update()
    {
        if (!_selectedPlane && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARRaycastHit hit = hits[0];
                    _selectedPlane = hit.trackable as ARPlane;
                    
                    // stuff when plane is selected
                    StartCanvas.SetActive(true);
                    
                    // disable plane manager and set other planes to inactive
                    planeManager.enabled = false;
                    foreach (ARPlane plane in planeManager.trackables)
                    {
                        if (plane != _selectedPlane)
                        {
                            plane.gameObject.SetActive(false);
                        }
                    }
                    
                    // stuff to do after other plans are gone
                    // NYI
                }
            }
        }
    }

    // getter for selected plane
    public ARPlane SelectedPlaneGetter()
    {
        if (!_selectedPlane)
        {
            throw new Exception("No plane selected");
        }
        return _selectedPlane;
    }
}
