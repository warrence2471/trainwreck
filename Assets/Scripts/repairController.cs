using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repairController : MonoBehaviour
{
    public string itemName;
    public GameObject replaceWith;

    public void Trigger() {
        if (replaceWith) {
            Instantiate(replaceWith, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
