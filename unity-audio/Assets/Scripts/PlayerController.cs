using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;

    public Transform camTransform;
    
    private CharacterController characterController;
    private float ySpeed;
    public Animator tyAnimator;
    private AudioSource tyAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //tyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalImput = Input.GetAxis("Horizontal");
        float verticalImput = Input.GetAxis("Vertical");
        
        if (tyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Getting Up") || tyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling Flat Impact") || tyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            horizontalImput = 0;
            verticalImput = 0;
        }

        // camera directions
        Vector3 camForwardDirection = camTransform.forward;
        Vector3 camRightDirection = camTransform.right;
        camForwardDirection.y = 0;
        camRightDirection.y = 0;
        
        // Disable character controller if ty is falling
        // if (tyAnimator.GetBool("IsFalling"))
        // {
        //     characterController.enabled = false;
        // }
        
        // create relative camera direction
        Vector3 forwardRelative = verticalImput * camForwardDirection;
        Vector3 rightRelative = horizontalImput * camRightDirection;

        Vector3 moveDitrection = forwardRelative + rightRelative;

        // Vector3 moveDitrection = new Vector3(horizontalImput, 0, verticalImput);
        float magnitude = Mathf.Clamp01(moveDitrection.magnitude) * speed;
        moveDitrection.Normalize();
        
        // jump and gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;
        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;
            
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                tyAnimator.SetBool("IsJump", true);
                tyAnimator.SetBool("IsRunning", false);
            }
            else
            {
                tyAnimator.SetBool("IsJump", false);
            }
            tyAnimator.SetBool("IsFalling", false);
        }

        Vector3 velocity = moveDitrection * magnitude;
        velocity.y = ySpeed;

        // run animate
        if ((characterController.velocity.x != 0 || characterController.velocity.z != 0) && characterController.isGrounded)
        {
            tyAnimator.SetBool("IsRunning", true);
        }
        else
        {
            tyAnimator.SetBool("IsRunning", false);
        }
        
        // move player
        characterController.Move(velocity * Time.deltaTime);

        if (moveDitrection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDitrection, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CheckFall();
    }

    void CheckFall()
    {
        if (transform.position.y < -2)
        {
            tyAnimator.SetBool("IsFalling", true);
        }
        if (transform.position.y < -15)
        {
            characterController.SimpleMove(Vector3.zero);
            characterController.transform.position = new Vector3(0, 30, 0);
        }
    }
}
