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
    public float yDist = .50f;
    public float defaultFOV = 60.0f;
    public float sprintFOV = 90.0f;
    private bool isSprinting = false;

    private float currentLerpTime;
    public float interpolationTime = 0.5f;

    public float vertLatency = 0.5f;
    public float maxVertOffset = 3.0f;
    public float horizDistance = 2.0f;

    public Camera cam;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        cam = GetComponent<Camera>();
        pRB = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Vector3 newCameraPos = player.transform.position;
        // var ySign = 0;
        // if (pRB.velocity.y > .5) ySign = 1;
        // else if (pRB.velocity.y < -.5) ySign = -1;


        // var yOffset = ySign * Mathf.Min(Mathf.Abs(pRB.velocity.y), playerController.speed) / (playerController.speed + playerController.sprintSpeed);
        // yOffset = Mathf.Clamp(yOffset, -maxVertOffset, maxVertOffset);


        // newCameraPos += horizDistance * new Vector3(Mathf.Cos(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)), yDist, Mathf.Sin(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)));

        // newCameraPos.y += yOffset * maxVertOffset;

        // // var newY = transform.position.y + Mathf.Min(1, Time.deltaTime / vertLatency) * (newCameraPos.y - transform.position.y);
        // transform.position = Vector3.Lerp(transform.position, newCameraPos, Time.deltaTime / vertLatency);

        // transform.LookAt(player.transform.position);

        Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)), yDist, Mathf.Sin(Mathf.Deg2Rad * -(playerPos.transform.eulerAngles.y + 90)));
        transform.position = player.transform.position + 3 * offset;
        transform.LookAt(player.transform.position);


        //particles
        var speedEm = speedLines.emission;
        if (pRB.velocity.magnitude > speedCutoff)
        {
            speedEm.enabled = true;
        }
        else if (speedLines.isEmitting)
        {
            speedEm.enabled = false;
        }

        //Sprinting
        if (playerController.IsSprinting() && !isSprinting)
        {
            isSprinting = true;
            currentLerpTime = 0;
            cam.fieldOfView = defaultFOV;

        }
        else if (!playerController.IsSprinting() && isSprinting)
        {
            isSprinting = false;
            currentLerpTime = 0;
        }

        //fov interp
        if (currentLerpTime < interpolationTime)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > interpolationTime)
            {
                currentLerpTime = interpolationTime;
            }
            float t = currentLerpTime / interpolationTime;
            float targetFOV = isSprinting ? sprintFOV : defaultFOV;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, t);
        }
    }



}
