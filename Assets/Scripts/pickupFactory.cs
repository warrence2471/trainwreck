using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupFactory : MonoBehaviour
{
    public List<GameObject> pickupList = new List<GameObject>(2);
    public int numberOfObjectsToGenerate = 20;

    public GameObject groundToSpawn;

    private void Awake()
    {
        Vector3 groundSize = groundToSpawn.transform.localScale / 2;
        for(int i = 0; i <= numberOfObjectsToGenerate; i++)
        {
            var x = Random.Range(-groundSize.x + 1, groundSize.x - 1);
            var z = Random.Range(-groundSize.z + 1, groundSize.z - 1);
            var itemIndex = Random.Range(0, pickupList.Count);
            var pos = new Vector3(x, 0, z);

            Instantiate(pickupList[itemIndex], pos, Quaternion.identity);
        }
    }
}
