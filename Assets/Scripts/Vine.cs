using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    public GoUp fairy;
    public int keyNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {



        if (other.GetComponent<PlayerController>())
        {
            switch (keyNumber)
            {
                case 1:
                    if (!GameController.local.firstKey)
                    {
                        return;
                    }
                    break;

                case 2:
                    if (!GameController.local.secondKey)
                    {
                        return;
                    }
                    break;

                case 3:
                    if (!GameController.local.thirdKey)
                    {
                        return;
                    }
                    break;
            }
            fairy.active = true;
        }

    }
}
