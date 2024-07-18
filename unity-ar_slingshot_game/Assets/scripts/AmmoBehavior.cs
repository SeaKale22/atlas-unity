using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    private GameManager gameManager;
    private bool launched = false;
    private Vector3 destination;
    
    // Update is called once per frame
    void Update()
    {
        if (launched)
        {
            this.transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed);
        }
    }

    public void Launch(Vector3 toLoacation)
    {
        this.destination = toLoacation;
        launched = true;
        StartCoroutine(DestroyAmmo());
    }
    
    private IEnumerator DestroyAmmo()
    {
        yield return new WaitForSeconds(4f);
        //gameManager.ammoLaunched = false;
        //Destroy(this.gameObject);
        gameManager.RespawnAmmo();
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
}
