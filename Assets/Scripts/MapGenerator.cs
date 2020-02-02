using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject railtrack;
    [SerializeField]
    public GameObject railturn;
    [SerializeField]
    public GameObject[] train;
    [SerializeField]
    public GameObject finish;

    private void Awake()
    {
        Instantiate(railtrack, new Vector3(-5, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-4, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-3, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-2, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-1, 0, 4), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-1, 0, 3), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-1, 0, 2), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-1, 0, 1), Quaternion.identity);
        Instantiate(railtrack, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(2, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(3, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(4, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(5, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(6, 0, 0), Quaternion.identity);

        Instantiate(finish, new Vector3(2, 0, 0), Quaternion.identity);

        foreach (var obj in train)
        {
            Instantiate(obj, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
