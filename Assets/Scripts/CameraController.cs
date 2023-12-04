using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 5.0f;

    public float speedCutoff = 12.0f;

    public ParticleSystem speedLines;
    private PlayerController playerController;
    private Rigidbody pRB;
    public GameObject playerPos;
    public float minimumDist = 3.0f;
    public float yDist = 2.0f;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        pRB = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // var yOffset = 0.0f;
        // if (pRB.velocity.y > .5)
        // {
        //     //lower camera
        //     yOffset = -3f;
        // }
        // else if (pRB.velocity.y < -.5)
        // {
        //     //raise camera, less than amount to lower by;
        //     yOffset = .5f;
        // }
        Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)), yDist, Mathf.Sin(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)));
        transform.position = player.transform.position + 3 * offset;
        transform.LookAt(player.transform.position);
        var speedEm = speedLines.emission;
        if (pRB.velocity.magnitude > speedCutoff)
        {
            speedEm.enabled = true;
        }
        else if (speedLines.isEmitting)
        {
            speedEm.enabled = false;
        }

    }
}
