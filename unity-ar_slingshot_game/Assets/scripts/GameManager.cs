using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject ammoPrefab;
    public GameObject mainCam;
    public PlaneSelectionManager planeSelectionManager;
    public int numOfTargets = 5;
    public AimTracker aimTracker;
    public bool ammoLaunched = false;
    public GameObject gameCanvas;
    public TMP_Text scoreText;

    private ARPlane _selectedPlane;
    private float minScale = 0.1f; // Minimum scale of the target
    private float maxScale = 1.0f; // Maximum scale of the target
    private float maxDistance = 10f; // Maximum distance for scaling
    private GameObject ammo;
    private int _score = 0;

    void Update()
    {
        scoreText.text = $"Score: {_score}";
    }

    public void StartGame()
    {
        // get the selected plane from the plane selection manager
        _selectedPlane = planeSelectionManager.SelectedPlaneGetter();
        
        if (!_selectedPlane)
        {
            Debug.Log("No selected plane");
        }
        else
        {
            // instantiate targets = num
            for (int i = 0; i < numOfTargets; i++)
            {
                InstantiateTarget();
            }
            
            // turn on aimTracker
            aimTracker.GameObject().SetActive(true);
            aimTracker.SelectedPlaneYSetter(_selectedPlane.GetComponent<Renderer>().bounds.min.y);
            
            // hide selected plane
            _selectedPlane.GameObject().SetActive(false);
            
            // spawn Ammo
            SpawnAmmo();
        }
        
        // turn on gameCanvas
        gameCanvas.SetActive(true);
    }
    
    // method for Instantiating a target
    private void InstantiateTarget()
    {
        // get the bounds of the selected plane
        Renderer planeRenderer = _selectedPlane.GetComponent<Renderer>();
        Bounds bounds = planeRenderer.bounds;
        
        // create random position from the bounds of selected plane
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);
        Vector3 randomPosition = new Vector3(randomX, bounds.min.y, randomZ);
        
        // instantiate the target prefab at random position
        GameObject target = Instantiate(targetPrefab, randomPosition, Quaternion.identity);
        
        // Scale target based on distance from camera to plane
        float distance = Vector3.Distance(target.transform.position, Camera.main.transform.position);
        float scaleFactor = Mathf.Clamp(distance / maxDistance, minScale, maxScale);
        target.transform.localScale *= scaleFactor;
        // makes sure target is on top of plane
        target.transform.position = new Vector3(target.transform.position.x, bounds.max.y, target.transform.position.z);
    }

    // method for Instantiating first ammo
    private void SpawnAmmo()
    {
        Vector3 ammoPos = mainCam.transform.position;
        ammoPos.z += 0.5f;
        ammoPos.y += -0.5f;
        ammo = Instantiate(ammoPrefab, ammoPos, Quaternion.identity);
        ammo.GetComponent<AmmoBehavior>().SetGameManager(this);

    }
    public void LaunchAmmo()
    {
        if (!ammoLaunched)
        {
            Vector3 toLocation = aimTracker.EndPosGetter();
            ammo.GetComponent<AmmoBehavior>().Launch(toLocation);
            ammoLaunched = true;
        }
    }

    public void Score()
    {
        ammoLaunched = false;
        _score += 1;
        Destroy(ammo);
        SpawnAmmo();
        RespawnAmmo();
    }

    public void RespawnAmmo()
    { 
        ammoLaunched = false;
        Destroy(ammo);
        SpawnAmmo();
    }
}
