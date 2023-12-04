using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 5.0f;

    private PlayerController playerController;
    public GameObject playerPos;
    public float minimumDist = 3.0f;
    public float yDist = 2.0f;
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();

    }

    void LateUpdate()
    {
        // Keep the camera at the offset distance but rotate it around the player
        // transform.position = player.transform.position;

        // Get the player's movement direction
        // float movementX = playerController.GetMovementX();
        // float movementZ = playerController.GetMovementZ();
        // if (movementX != 0 || movementZ != 0)
        // {
        //     xDir = movementX != 0 ? (int)Mathf.Sign(movementX) : 0;
        // }
        Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)), yDist, Mathf.Sin(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)));
        transform.position = player.transform.position + 3 * offset;
        transform.LookAt(player.transform.position);
    }
}
