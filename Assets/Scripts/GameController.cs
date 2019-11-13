using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public delegate void RestartDelegate();
    public static RestartDelegate RestartEvent;


    public GameObject canvas;
    public PlayerController player;

    public delegate void DeathDelegate(int level);
    public static DeathDelegate DeathEvent;


    public static GameController local;
    public Transform spawnPoint;

    public bool firstKey;
    public bool secondKey;
    public bool thirdKey;
    // Start is called before the first frame update
    void Awake()
    {
        local = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        InputListener.PauseEvent += Pause;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        GoalPost.SceneChangeEvent += NextScene;
    }

    public void Start()
    {
        canvas.SetActive(false);
    }


    public static void Restart()
    {
        RestartEvent?.Invoke();
        DeathEvent?.Invoke(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame


    public void NextScene()
    {
        InputListener.PauseEvent -= Pause;
        GoalPost.SceneChangeEvent -= NextScene;
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
