using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    private float age = 0.0f;

    // Start is called before the first frame update
    void Awake()
    {
        transform.localScale /= 2;
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        transform.position += new Vector3(0, Time.deltaTime / 3, 0);
        transform.localScale += new Vector3(Time.deltaTime / 3, Time.deltaTime / 3, Time.deltaTime / 3);

        this.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1 / (age + 1));

        if (age > 6.0f) {
          Destroy(this.gameObject);
        }
    }
}
