using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupFactory : MonoBehaviour
{
    public List<GameObject> pickupList = new List<GameObject>(2);
    public int numberOfObjectsToGenerate = 20;

    public float maxLeft = 100;
    public float maxTop = 100;

    private void Awake()
    {
        for(int i = 0; i <= numberOfObjectsToGenerate; i++)
        {
            var x = Random.Range(-maxLeft, maxLeft);
            var z = Random.Range(-maxTop, maxTop);
            var itemIndex = Random.Range(0, pickupList.Count);
            var pos = new Vector3(x, 0, z);

            Instantiate(pickupList[itemIndex], pos, Quaternion.identity);
        }
    }
}
