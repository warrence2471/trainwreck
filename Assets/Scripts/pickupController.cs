using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupController : MonoBehaviour
{
    public string itemName;

    private float originY;


    private void Awake()
    {
        originY = transform.position.y;
        transform.localScale = new Vector3(.4f, .4f, .4f);
    }

    private void Update()
    {
        var newY = originY + Mathf.Sin(Time.time) * .05f + .1f;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(new Vector3(0, Time.deltaTime * 180, 0));
    }
}
