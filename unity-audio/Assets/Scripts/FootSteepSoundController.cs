using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteepSoundController : MonoBehaviour
{
    public AudioClip stoneFootsetps;
    public AudioClip grassFootsteps;
    public AudioClip rockImpact;

    private Animator tyAnimator;
    private AudioSource tyFootsteps;

    private bool hasThumped;
    
    // Start is called before the first frame update
    void Start()
    {
        tyAnimator = GetComponentInChildren<Animator>();
        tyFootsteps = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (tyAnimator.GetBool("IsRunning"))
        {
            if (!tyFootsteps.isPlaying)
            {
                tyFootsteps.Play();
            }

            hasThumped = false;
        }
        else
        {
            tyFootsteps.Pause();
        }
        
        //play thump if ty falling impact is playing
        if (hasThumped == false && tyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling Flat Impact"))
        {
            Debug.Log("Landed");
            tyFootsteps.clip = rockImpact;
            Debug.Log("Set clip");
            tyFootsteps.Play();
            Debug.Log("Played Sound");
            hasThumped = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Debug.Log("Collision Detected");
        // Debug.Log($"IsRunning: {tyAnimator.GetBool("IsRunning")}");
        if (tyAnimator.GetBool("IsRunning"))
        {
            if (hit.gameObject.CompareTag("stone"))
            {
               // Debug.Log("Walking on Stone");
                tyFootsteps.clip = stoneFootsetps;
            }
            else if (hit.gameObject.CompareTag("grass"))
            {
               // Debug.Log("Walking on Grass");
                tyFootsteps.clip = grassFootsteps;
            }
            // else
            // {
            //     Debug.Log($"Tag: {hit.gameObject.tag}");
            // }
        }
    }
}
