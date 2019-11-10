using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject bullet;
    
    public float bulletSpeed;
    public float fireDelay;
    public float timeNoise;
    public float bulletLifeTime;
    public float initialTimer;

    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = initialTimer;
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
        timer = initialTimer;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= fireDelay)
        {
            timer = 0f;
            GameObject bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
            bulletInstance.transform.localScale = transform.localScale*2f;
            bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * bulletSpeed, ForceMode.VelocityChange);
            Destroy(bulletInstance, bulletLifeTime);
        }

    }
}
