using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float rotationSpeed = 100; // Speed of rotation
    public GameObject playerPos;
    private Rigidbody rb;
    private float rotationInput;
    private float forwardInput;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        // Calculate rotation
        float rotationAngle = rotationInput * rotationSpeed * Time.deltaTime;
        playerPos.transform.Rotate(0, rotationAngle, 0);
        // Calculate force direction
        Vector3 forceDirection = playerPos.transform.forward * forwardInput * speed;
        rb.AddForce(forceDirection);
        // Debug.Log(rb.velocity.magnitude);
    }

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f; // Checks if the player is moving
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
