using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var train = other.GetComponent<GameTrain>();
        if (train != null)
        {
            train.StopTrain();
        }
    }
}
