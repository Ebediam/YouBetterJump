using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCharge : MonoBehaviour
{
    public PlayerController player;
    public ParticleSystem takeVFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.GetComponentInChildren<PlayerController>())
        {
            player.secondaryJump = true;
            takeVFX.transform.parent = null;
            takeVFX.Play();
            
            gameObject.SetActive(false);
        }
    }
}
