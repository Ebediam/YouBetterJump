using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    
    public bool isActive = true;
    public Material eyeMaterial;
    public Material bodyMaterial;
    public new Light light;

    public void Start()
    {
        eyeMaterial.SetFloat("_hasDoubleJump", 0);
        bodyMaterial.SetFloat("_hasDoubleJump", 0);
        light.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            return;
        }

        if (other.transform.root.GetComponentInChildren<PlayerController>())
        {
            GameController.local.transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            eyeMaterial.SetFloat("_hasDoubleJump", 1);
            bodyMaterial.SetFloat("_hasDoubleJump", 1);
            light.enabled = true;
        }
    }

}
