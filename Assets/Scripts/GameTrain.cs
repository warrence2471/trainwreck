using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrain : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed = 1.5f;

    [SerializeField]
    public GameObject smoke;

    private float age = 0.0f;
    private float smokeAge = 0.2f;
    private bool isDriving = false;
    private float currentSpeed = 0;
    private float acceleration = 0.1f;
    private float negativeAcceleration = 0.3f;

    private Rigidbody myRigidbody;
    private Transform myTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();

        startTrain();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDriving)
        {
            age += Time.deltaTime;
            if (age > smokeAge)
            {
                age = 0;
                Instantiate(smoke, new Vector3(transform.position.x + 0.4f, transform.position.y + 0.4f, transform.position.z), Quaternion.identity);
            }

            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= negativeAcceleration * Time.deltaTime;
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        myTransform.position = myTransform.position + myTransform.forward * currentSpeed / 120;
    }

    private void OnTriggerEnter(Collider other)
    {
        myTransform.Rotate(0, 25f, 0);
    }

    public void startTrain()
    {
        this.isDriving = true;
    }

    public void stopTrain()
    {
        this.isDriving = false;
    }
}
