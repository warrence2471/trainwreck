using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    private float age = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        transform.position += new Vector3(0, 0, Time.deltaTime / 2);
        if (age > 2.0f) {
          Destroy(this.gameObject);
        }
    }
}
