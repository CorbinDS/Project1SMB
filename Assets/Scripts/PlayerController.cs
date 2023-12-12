using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float sprintSpeed = 5;
    public float rotationSpeed = 100; // Speed of rotation
    public GameObject playerPos;
    private Rigidbody rb;
    private float rotationInput;
    private float forwardInput;
    public float sprintMeter;
    public float sprintDec = .4f;
    public float sprintRegen = .2f;
    private bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sprintMeter = 1.0f;
        sprintDec = .4f;
        sprintRegen = .2f;
    }

    void OnMove(InputValue movementVal)
    {
        Vector2 movementVector = movementVal.Get<Vector2>();
        if (movementVector.x > 0)
        {
            rotationInput = 1;
        }
        else if (movementVector.x < 0)
        {
            rotationInput = -1;
        }
        else
        {
            rotationInput = 0;
        }

        if (movementVector.y > 0)
        {
            forwardInput = 1;
        }
        else if (movementVector.y < 0)
        {
            forwardInput = -1;
        }
        else
        {
            forwardInput = 0;
        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) && sprintMeter > 0.0f)
        {
            sprintMeter -= sprintDec * Time.deltaTime;
            if (sprintMeter < 0) sprintMeter = 0.0f;
            sprinting = true;
        }
        else if (sprintMeter < 1.0f)
        {
            sprintMeter += sprintRegen * Time.deltaTime;
            if (sprintMeter > 1.0f) sprintMeter = 1.0f;
            sprinting = false;

        }


        // Calculate rotation
        float rotationAngle = rotationInput * rotationSpeed * Time.deltaTime;
        playerPos.transform.Rotate(0, rotationAngle, 0);

        Vector3 forceDirection = playerPos.transform.forward * forwardInput * speed;

        // Calculate force direction
        if (sprinting)
        {
            forceDirection = playerPos.transform.forward * forwardInput * (speed + sprintSpeed);
        }
        rb.AddForce(forceDirection);


        // Debug.Log(rb.velocity.magnitude);
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f; // Checks if the player is moving
    }

    public bool IsSprinting()
    {
        return sprinting;
    }

    public float SprintAmount()
    {
        return sprintMeter;
    }
    // public float GetMovementX()
    // {
    //     return movementX;
    // }

    // public float GetMovementZ()
    // {
    //     return movementY;
    // }
}
