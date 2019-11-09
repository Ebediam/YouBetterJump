using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalPost : MonoBehaviour
{
    public delegate void SceneChangeDelegate();
    public static SceneChangeDelegate SceneChangeEvent;

    private void OnTriggerEnter(Collider other)
    {
        SceneChangeEvent?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
