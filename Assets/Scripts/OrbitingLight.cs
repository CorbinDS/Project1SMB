using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingLight : MonoBehaviour
{
    public GameObject targetObject;  // The target object around which the light will orbit
    public float orbitSpeed = 5f;    // Speed of the orbit
    public float orbitRadius = 3f;   // Radius of the orbit

    private float orbitAngle = 0f;   // Current angle of the orbit

    void Update()
    {
        if (targetObject == null)
            return;

        // Calculate the new position of the light
        orbitAngle += orbitSpeed * Time.deltaTime;
        float x = targetObject.transform.position.x + orbitRadius * Mathf.Cos(orbitAngle);
        float z = targetObject.transform.position.z + orbitRadius * Mathf.Sin(orbitAngle);
        float y = targetObject.transform.position.y;  // You can adjust the Y value if needed

        // Update the light's position
        transform.position = new Vector3(x, y, z);

        // Optionally, make the light look at the target object
        transform.LookAt(targetObject.transform);
    }
}
