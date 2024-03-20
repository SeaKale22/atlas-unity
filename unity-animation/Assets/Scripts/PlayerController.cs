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
    private Animator tyAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        tyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    
        float horizontalImput = Input.GetAxis("Horizontal");
        float verticalImput = Input.GetAxis("Vertical");

        // if (verticalImput + horizontalImput > 0)
        // {
        //     tyAnimator.SetBool("IsRunning", true);
        // }
        // else
        // {
        //     tyAnimator.SetBool("IsRunning", false);
        // }
        
        // camera directions
        Vector3 camForwardDirection = camTransform.forward;
        Vector3 camRightDirection = camTransform.right;
        camForwardDirection.y = 0;
        camRightDirection.y = 0;
        
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
            }
        }

        Vector3 velocity = moveDitrection * magnitude;
        velocity.y = ySpeed;

        // run animate
        if (velocity.x != 0)
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
        if (transform.position.y < -10)
        {
            characterController.SimpleMove(Vector3.zero);
            characterController.transform.position = new Vector3(0, 15, 0);
        }
    }
}
