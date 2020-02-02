using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmitter : MonoBehaviour
{
    [SerializeField]
    public bool smokeActive = false;
    [SerializeField]
    public float smokeFrequency = 0.2f;
    [SerializeField]
    public GameObject smoke;
    
    private float age = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (!smokeActive) return;

        age += Time.deltaTime;
        if (age > smokeFrequency)
        {
            age = 0;
            float xSpin = Random.Range(0, 360);
            float ySpin = Random.Range(0, 360);
            float zSpin = Random.Range(0, 360);
            Instantiate(smoke, transform.position + 0.2f * transform.forward + 0.5f * transform.up, Quaternion.Euler(xSpin, ySpin, zSpin));
        }
    }
}
