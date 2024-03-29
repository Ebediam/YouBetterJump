﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputListener : MonoBehaviour
{
    public delegate void PauseDelegate(bool paused);
    public static PauseDelegate PauseEvent;

    public bool paused = false;

    public PlayerControls controls;
    public bool menu;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();

        if (menu)
        {
            controls.GamePlay.Jump.performed += ChangeScene;
            controls.GamePlay.Pause.performed += ExitGame;
        }
        {
            controls.GamePlay.Pause.performed += Pause;
            controls.GamePlay.Jump.performed += ResetLevel;
        }


    }

    // Update is called once per frame
    public void ChangeScene(InputAction.CallbackContext context)
    {
        
        SceneManager.LoadScene(1);
    }

    public void Pause(InputAction.CallbackContext context)
    {
        paused = !paused;
        PauseEvent?.Invoke(paused);

    }

    public void ExitGame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    public void ResetLevel(InputAction.CallbackContext context)
    {
        if (paused)
        {
            GameController.Restart();
            Pause(context);
        }
    }

    public void OnEnable()
    {
        controls.GamePlay.Enable();

    }

    public void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
