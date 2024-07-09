using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public PlaneSelectionManager planeSelectionManager;
    public int numOfTargets = 5;
    public float targetMoveSpeed = 1f;

    private ARPlane _selectedPlane;

    private void Start()
    {
        // get the selected plane from the plane selection manager
        _selectedPlane = planeSelectionManager.SelectedPlaneGetter();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
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
        }
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
        Vector3 randomPosition = new Vector3(randomX, bounds.max.y, randomZ);
        
        // instantiate the target prefab at random position
        GameObject target = Instantiate(targetPrefab, randomPosition, Quaternion.identity);
        
        // create a random direction for the target
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        // start moving the newly created target
        MoveTarget(target, randomDirection);
    }

    private void MoveTarget(GameObject target, Vector3 direction)
    {
        // move target in given direction
        target.transform.Translate(direction * targetMoveSpeed * Time.deltaTime, Space.World);
        
        // clamp in plane bounds
        Vector3 targetPosition = target.transform.position;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -5f, 5f);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -5f, 5f);
        target.transform.position = targetPosition;
        
        // setup for and invoke change direction
        float randomWait = Random.Range(1f, 3f);
        Invoke(nameof(ChangeDirection), randomWait);
    }

    private void ChangeDirection()
    {
        // create new random direction
        Vector3 newDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        
        // get list of targets and choose random one to change direction
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        if (targets.Length > 0)
        {
            GameObject randomTarget = targets[Random.Range(0, targets.Length)];
            MoveTarget(randomTarget, newDirection);
        }
        else
        {
            Debug.Log("No targets found");
        }
    }
}
