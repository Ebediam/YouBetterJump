using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCharge : MonoBehaviour
{
    public PlayerController player;
    public ParticleSystem takeVFX;
    public List<MeshRenderer> meshes;
    public Collider col;

    public void Start()
    {
        GameController.RestartEvent += Restart;
        GoalPost.SceneChangeEvent += SceneChange;

    }

    public void SceneChange()
    {
        GameController.RestartEvent -= Restart;
        GoalPost.SceneChangeEvent -= SceneChange;

    }

    public void Restart()
    {
        SwitchActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.GetComponentInChildren<PlayerController>())
        {
            player.secondaryJump = true;
            takeVFX.Play();
            SwitchActive(false);           
            
        }
    }

    public void SwitchActive(bool active)
    {
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = active;
        }
        col.enabled = active;
    }
}
