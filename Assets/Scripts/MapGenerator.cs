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
    public GameObject railtrackBroken;
    [SerializeField]
    public GameTrain[] train;
    [SerializeField]
    public GameObject finish;

    private void Awake()
    {
        // Those are the starting tracks for the train in the game scene
        Instantiate(railtrack, new Vector3(-14, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-13, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-12, 0, 0), Quaternion.identity);



        Instantiate(railtrack, new Vector3(-5, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-4, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-3, 0, 5), Quaternion.identity);
        Instantiate(railtrack, new Vector3(-2, 0, 5), Quaternion.identity);
        Instantiate(railturn, new Vector3(-1, 0, 5), Quaternion.Euler(0, 180, 0));
        Instantiate(railtrack, new Vector3(-1, 0, 4), Quaternion.Euler(0, 90, 0));
        Instantiate(railtrack, new Vector3(-1, 0, 3), Quaternion.Euler(0, 90, 0));
        Instantiate(railtrack, new Vector3(-1, 0, 2), Quaternion.Euler(0, 90, 0));
        Instantiate(railtrack, new Vector3(-1, 0, 1), Quaternion.Euler(0, 90, 0));
        Instantiate(railturn, new Vector3(-1, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(1, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(2, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(3, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(4, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(5, 0, 0), Quaternion.identity);
        Instantiate(railtrack, new Vector3(6, 0, 0), Quaternion.identity);

        Instantiate(finish, new Vector3(2, 0, 0), Quaternion.identity);

        // TODO Spawning the train creates problems with the wagon links, no idea what's going on...
        //for (int i = 0; i < train.Length; i++)
        //{
        //    var follower = Instantiate(train[i], new Vector3(-2.5f - i * GameTrain.CWagonDistance, 0, 5), Quaternion.Euler(0, 90, 0));
        //    if (i > 0)
        //    {
        //        //follower.GetComponent<GameTrain>().preceding = train[i - 1];
        //    }
        //}
    }
}
