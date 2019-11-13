using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    public float acceleration;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GameController.local.player.acceleration = acceleration;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            GameController.local.player.acceleration = 0f;
        }
    }
}
