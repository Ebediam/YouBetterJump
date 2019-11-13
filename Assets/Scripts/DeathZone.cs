using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public bool ignorePlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (ignorePlayer)
        {
            return;
        }
        if (other.GetComponentInChildren<PlayerController>())
        {
            GameController.Restart();
        }
    }
}
