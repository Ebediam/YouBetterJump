using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationRadius : MonoBehaviour
{
    public Enemy enemy;
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.GetComponent<PlayerController>())
        {

            enemy.canMove = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.GetComponent<PlayerController>())
        {

            enemy.canMove = false;
        }
    }

}
