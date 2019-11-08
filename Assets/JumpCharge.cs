using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCharge : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.GetComponentInChildren<PlayerController>())
        {
            player.secondaryJump = true;
            gameObject.SetActive(false);
        }
    }
}
