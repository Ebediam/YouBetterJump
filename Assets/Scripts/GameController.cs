using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void RestartDelegate();
    public static RestartDelegate RestartEvent;
    public GameObject canvas;

    

    public static GameController local;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        local = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        InputListener.PauseEvent += Pause;
        canvas.SetActive(false);
    }

    public static void Restart()
    {
        RestartEvent?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause(bool paused)
    {
        canvas.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
