using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficLightController : MonoBehaviour
{
    public bool isGreen = false;

    public void toggleLights()
    {
        if(isGreen)
        {
            isGreen = false;
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            isGreen = true;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
