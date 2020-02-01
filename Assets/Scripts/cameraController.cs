using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    
    public float speed = 3.0f;
    public float offset = 5.0f;
    
    void Update () {
        float interpolation = speed * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.z = Mathf.Lerp(this.transform.position.z, objectToFollow.transform.position.z - offset, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
        
        this.transform.position = position;
    }
}
