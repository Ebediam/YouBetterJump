using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform spawnPoint;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<PlayerController>())
        {
            other.transform.root.position = spawnPoint.position;
            other.transform.root.GetComponentInChildren<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
