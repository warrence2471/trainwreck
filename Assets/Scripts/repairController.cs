using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repairController : MonoBehaviour
{
    public List<string> itemsNeededToRepair = new List<string>(1);
    public GameObject replaceWith;

    public void Trigger() {
        if (replaceWith) {
            Instantiate(replaceWith, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
