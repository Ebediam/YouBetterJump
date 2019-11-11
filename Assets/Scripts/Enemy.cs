using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int frames;
    public int switchFrames;
    public bool isStatic = false;
    public Transform startPosition;
    public bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        GameController.RestartEvent += Reset;
        GoalPost.SceneChangeEvent += ChangeScene;
    }

    public void Reset()
    {
        transform.position = startPosition.position;
        canMove = false;
    }
    // Update is called once per frame

    public void ChangeScene()
    {
        GameController.RestartEvent -= Reset;
        GoalPost.SceneChangeEvent -= ChangeScene;
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }


        frames++;
        if(frames >= switchFrames)
        {
            isStatic = !isStatic;
            frames = 0;
        }

        if (!isStatic)
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }

    }


}
