using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{

    public MeshRenderer mesh;
    public Collider col;
    public ParticleSystem pickVFX;
    public PlayerController player;

    public delegate void KeyCollectedDelegate(int level);
    public static KeyCollectedDelegate KeyCollectedEvent;

    // Start is called before the first frame update

    public void Start()
    {

        GameController.RestartEvent += Restart;
        GoalPost.SceneChangeEvent += SceneChange;
        player = GameController.local.player;
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


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponentInChildren<PlayerController>())
        {
            pickVFX.Play();
            SwitchActive(false);
            KeyCollectedEvent?.Invoke(SceneManager.GetActiveScene().buildIndex);

        }
    }

    public void SwitchActive(bool active)
    {
        mesh.enabled = active;
        col.enabled = active;
    }
}
