using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainMainMenuBehaviour : MonoBehaviour
{
    private float startYposition;

    private void Awake()
    {
        this.startYposition = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        var newYposition = this.startYposition + (Mathf.Sin(Time.timeSinceLevelLoad * 10) + 1) / 40;
        this.transform.position = new Vector3(this.transform.position.x, newYposition, this.transform.position.z);
    }
}
