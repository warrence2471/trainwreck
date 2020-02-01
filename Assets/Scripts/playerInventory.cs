using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public List<string> items = new List<string>(2);
    public GameObject selectedPickup;


    private bool hasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void addItem(string item, int slot)
    {
        items[slot] = item;
    }

    public bool useItem(string itemName)
    {
        if(hasItem(itemName))
        {
            items.Remove(itemName);
            return true;
        }

        return false;
    }

    public string getItemInSlot(int slot)
    {
        return items[slot];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "pickup") {
                    this.selectedPickup = hit.collider.gameObject;
                }
            }
        }


        if (selectedPickup)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                addItem(selectedPickup.GetComponent<pickupController>().itemName, 0);
                Destroy(selectedPickup.gameObject);
                selectedPickup = null;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                addItem(selectedPickup.GetComponent<pickupController>().itemName, 1);
                Destroy(selectedPickup.gameObject);
                selectedPickup = null;
            }
        }
    }
}
