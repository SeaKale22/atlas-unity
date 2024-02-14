using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

// Ensures we are on the player object
[RequireComponent(typeof(PlayerController))]
public class Teleportation : MonoBehaviour
{
    // PlayerController player;
    [SerializeField] private float cooldownDuration;
    bool isOnCooldown = false;
    private float yValue;

    private void Start()
    {
        // player = this.gameObject.GetComponent<PlayerController>();
        yValue = this.transform.position.y;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Teleporter"))
        {
            go.GetComponent<TeleporterObject>().Instantiate(this);
        }
    }

    public void Trigger(Vector3 targetLocation)
    {
        if (!isOnCooldown)
        {
            TeleportPlayer(targetLocation);
            Debug.Log("Teleported!");
            StartCoroutine(CoolDown(cooldownDuration));
        }
        else
        {
            Debug.Log("Teleports are on cooldown!");
        }
    }

    IEnumerator CoolDown (float duration)
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(duration);
        isOnCooldown = false;
    }
    void TeleportPlayer(Vector3 targetPosition)
    {
        this.transform.position = new Vector3(targetPosition.x, yValue, targetPosition.z);
    }
}
