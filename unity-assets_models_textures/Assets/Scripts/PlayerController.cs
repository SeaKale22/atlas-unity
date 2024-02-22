using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    
    private CharacterController characterController;
    private float ySpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalImput = Input.GetAxis("Horizontal");
        float verticalImput = Input.GetAxis("Vertical");

        Vector3 moveDitrection = new Vector3(horizontalImput, 0, verticalImput);
        float magnitude = Mathf.Clamp01(moveDitrection.magnitude) * speed;
        moveDitrection.Normalize();

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

        characterController.Move(velocity * Time.deltaTime);

        if (moveDitrection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDitrection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
