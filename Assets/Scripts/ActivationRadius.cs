using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationRadius : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponentInChildren<PlayerController>())
        {
            enemy.canMove = true;
        }
    }
}
