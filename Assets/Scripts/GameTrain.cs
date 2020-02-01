using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrain : MonoBehaviour
{
    [SerializeField]
    float MaxSpeed = 3f;
    
    Rigidbody myRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(MaxSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
