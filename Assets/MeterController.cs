using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private Image sprintMeter;

    private PlayerController playerController;
    void Start()
    {
        sprintMeter = GetComponent<Image>();
        playerController = player.GetComponent<PlayerController>();
        sprintMeter.fillAmount = 1;
    }

    void LateUpdate()
    {
        sprintMeter.fillAmount = playerController.SprintAmount();
    }
}
