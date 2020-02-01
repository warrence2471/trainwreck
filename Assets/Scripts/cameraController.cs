using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject objectToFollow;
    
    public float speed_x = 3.0f;
    public float speed_z = 7.0f;
    public float offset = 5.0f;
    
    void Update () {
        float interpolation_x = speed_x * Time.deltaTime;
        float interpolation_z = speed_z * Time.deltaTime;
        
        Vector3 position = this.transform.position;
        position.z = Mathf.Lerp(this.transform.position.z, objectToFollow.transform.position.z - offset, interpolation_z);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation_x);
        
        this.transform.position = position;
    }
}
