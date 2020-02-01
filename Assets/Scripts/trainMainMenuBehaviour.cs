using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainMainMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private float startYposition;
    [SerializeField]
    public GameObject smoke;
    private float age = 0.0f;
    private float smokeAge = 0.2f;
    private float currentSpeed = 0;
    public float maxSpeed = 100;
    public float acceleration = 1;
    public float negativeAcceleration = 3;
    public bool isDriving = false;

    private void Awake()
    {
        this.startYposition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDriving)
        {
          age += Time.deltaTime;
          if (age > smokeAge / (currentSpeed * 20 + 0.01f)) {
            age = 0;
            Instantiate(smoke, new Vector3(transform.position.x+.6f, transform.position.y+1.2f, transform.position.z-.3f), Quaternion.identity);
          }
        }

        if (isDriving && currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }


        if(!isDriving && currentSpeed > 0)
        {
            currentSpeed -= negativeAcceleration * Time.deltaTime;
        }

        if(currentSpeed < 0)
        {
            currentSpeed = 0;
        }


        var newYposition = startYposition;
        if (currentSpeed == 0) {
            newYposition = startYposition + (Mathf.Sin(Time.timeSinceLevelLoad * 20) + 1) / 200;
        }

        var newXposition = transform.position.x + currentSpeed;
        transform.position = new Vector3(newXposition, newYposition, transform.position.z);
    }

    public void startTrain()
    {
        this.isDriving = true;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources) {
            audioSource.Play();
        }
    }

    public void stopTrain()
    {
        this.isDriving = false;
    }
}
