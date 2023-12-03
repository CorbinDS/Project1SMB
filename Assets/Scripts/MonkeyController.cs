using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private Vector3 lastPosition;
    private float rotation = 0;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        // xDiff = lastPosition.x - transform.position.x;
        if (lastPosition.x < transform.position.x - .25 * Time.deltaTime)
        {
            rotation += 1;
        }
        else if (lastPosition.x > transform.position.x + .25 * Time.deltaTime)
        {
            rotation -= 1;
        }
        lastPosition = transform.position;

        Vector3 eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.position = player.transform.position + offset;
    }
}
