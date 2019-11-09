using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public KeyScript keyScript;
    public Image icon;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
        if (keyScript.unlocked)
        {            
            icon.sprite = keyScript.fullKey;
        }
        else
        {
            icon.sprite = keyScript.emptyKey;
        }


    }

    public void Initialize()
    {
        GoalPost.SceneChangeEvent += OnSceneChange;
        GameController.DeathEvent += OnDeath;
        Key.KeyCollectedEvent += OnKeyCollected;
    }

    public void OnDeath(int level)
    {
        if (level == keyScript.level)
        {
            keyScript.unlocked = false;
            icon.sprite = keyScript.emptyKey;
        }
    }

    public void OnSceneChange()
    {
        Key.KeyCollectedEvent -= OnKeyCollected;
        GoalPost.SceneChangeEvent -= OnSceneChange;
        GameController.DeathEvent -= OnDeath;
    }

    public void OnKeyCollected(int sceneNumber)
    {
        if(sceneNumber == keyScript.level)
        {
            keyScript.unlocked = true;
            icon.sprite = keyScript.fullKey;
        }
    }
}
