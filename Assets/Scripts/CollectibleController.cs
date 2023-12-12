using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public float bobSpeed = 1.0f;
    public float bobPercent = .6f;



    private float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        transform.Rotate(0, rotationSpeed, 0);
        pos.y = startY + bobPercent * Mathf.Sin(Time.time * bobSpeed);
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            ScoreManager.Instance.IncreaseScore(1);
        }
    }
}
