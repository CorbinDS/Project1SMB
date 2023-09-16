using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // float yRotation = Mathf.Acos(Vector3.Dot(rb.velocity.normalized, transform.forward)) * Mathf.Rad2Deg;

        // // Debug.Log(yRotation);
        // // transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
        // transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), yRotation * Time.deltaTime);
        // offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;

    }
}
