using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            return;
        }

        GameController.local.transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }

}
