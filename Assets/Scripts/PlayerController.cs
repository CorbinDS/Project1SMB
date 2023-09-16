using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementVal)
    {
        Vector2 movementVector = movementVal.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    // Update is called once per frame
    // void Update()
    // {
    //     float moveSpeed = 3f;
    //     var rotateStep = moveSpeed * Time.deltaTime;
    //     Vector3 moveDir = new Vector3(0, 0, 0);
    //     Quaternion rotateDir = new Quaternion(0, 0, 0, 0);

    //     if (Input.GetKey(KeyCode.W))
    //     {
    //         moveDir.z = +1f;
    //         rotateDir.x = +1f;
    //     }
    //     if (Input.GetKey(KeyCode.S)) { moveDir.z = -1f; }

    //     if (Input.GetKey(KeyCode.A)) { moveDir.x = -1f; }
    //     if (Input.GetKey(KeyCode.D)) { moveDir.x = +1f; }

    //     transform.position += moveDir * moveSpeed * Time.deltaTime;
    //     transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDir, rotateStep);
    // }
}
