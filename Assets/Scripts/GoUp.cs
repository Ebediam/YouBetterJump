using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoUp : MonoBehaviour
{

    public JumpCharge fairyBrain;
    public bool active;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            fairyBrain.isTrapped = false;
            fairyBrain.animator.SetBool("isTrapped", false);
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }
}
