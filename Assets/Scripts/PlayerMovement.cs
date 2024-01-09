using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rgb;
        
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumperForce = 20f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    
    [SerializeField] float dashDistance = 5f;
    [SerializeField] float dashTime = 0.5f;
    private bool isDashing = false;

    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rgb.velocity = new Vector3(horizontalInput * movementSpeed, rgb.velocity.y, verticalInput * movementSpeed);
        
        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            {
                Vector3 dashDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                StartCoroutine(Dash(dashDirection));
            }
        }
    }

    void Jump()
    {
        rgb.velocity = new Vector3(rgb.velocity.x, jumpForce, rgb.velocity.z);
    }

    IEnumerator  Dash(Vector3 dashDirection)
    {
        isDashing = true;
        float dashTimer = 0f;

        while (dashTimer < dashTime)
        {
            transform.Translate(dashDirection * dashDistance * (Time.deltaTime / dashTime), Space.World);
            dashTimer += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
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