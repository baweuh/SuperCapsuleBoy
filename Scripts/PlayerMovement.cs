using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rgb;
        
    public float movementSpeed = 6f;

    public Transform cameraTransform;
    public Transform bodyTransform;

    public float yawRotationSpeed;
    public float pitchRotationSpeed;
    
    public float jumpForce = 5f;
    public float jumperForce = 20f;

    public Transform groundCheck;
    public LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;

        Vector3 moveDirection = (cameraForward * verticalInput + cameraTransform.right * horizontalInput).normalized;

        rgb.velocity = new Vector3(moveDirection.x * movementSpeed, rgb.velocity.y, moveDirection.z * movementSpeed);
        
        var mouseXDelta = Input.GetAxis("Mouse X");

        bodyTransform.Rotate(Vector3.up, Time.deltaTime * yawRotationSpeed * mouseXDelta);

        var mouseYDelta = Input.GetAxis("Mouse Y");

        var rotation = cameraTransform.localRotation;

        var rotationX = rotation.eulerAngles.x;

        rotationX += -Time.deltaTime * pitchRotationSpeed * mouseYDelta;
        

        var unClampedRotationX = rotationX;

        if (unClampedRotationX >= 180)
        {
            unClampedRotationX -= 360;
        }

        var clampedRotationX = Mathf.Clamp(unClampedRotationX, -60, 60);

        cameraTransform.localRotation =
            Quaternion.Euler(new Vector3(
                clampedRotationX,
                rotation.eulerAngles.y,
                rotation.eulerAngles.z
            ));
        
        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            Jump();
        }
        
    }

    void Jump()
    {
        rgb.velocity = new Vector3(rgb.velocity.x, jumpForce, rgb.velocity.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }

        if (collision.gameObject.CompareTag("Jumper"))
        {
            rgb.velocity = new Vector3(rgb.velocity.x, jumperForce, rgb.velocity.z);
        }
    }

    bool CheckGround()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}