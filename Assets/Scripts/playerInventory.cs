using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public AudioClip missingItems;
    public List<string> items = new List<string>(2);

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
        if (hasItem(itemName))
        {
            int index = items.IndexOf(itemName);
            items[index] = null;
            return true;
        }

        return false;
    }

    public string getItemInSlot(int slot)
    {
        return items[slot];
    }

    // Handle Key Inputs
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Repair(GetTarget());
            Pickup(GetTarget(), 1);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pickup(GetTarget(), 0);
            Debug.Log("pressed Q");
        }
    }

    void Pickup(GameObject item, int slot)
    {
        if (!item || item.tag != "pickup") return;
        addItem(item.GetComponent<pickupController>().itemName, slot);
        Destroy(item.gameObject);
    }

    void Repair(GameObject item)
    {
        if (!item || item.tag != "repair") return;
        repairController repairC = item.GetComponent<repairController>();

        List<string> neededItems = repairC.itemsNeededToRepair;
        
        foreach(string neededItem in neededItems) {
            if (!hasItem(neededItem)) {
                if (missingItems) AudioSource.PlayClipAtPoint(missingItems, item.transform.position);
                return;
            }
        }

        neededItems.ForEach(i => useItem(i));
        repairC.Trigger();
    }

    GameObject GetTarget()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 fwd2 = transform.TransformDirection(Vector3.forward + new Vector3(.25f, 0, 0));
        Vector3 fwd3 = transform.TransformDirection(Vector3.forward + new Vector3(-.25f, 0, 0));
        Vector3 fwd4 = transform.TransformDirection(Vector3.forward + new Vector3(.5f, 0, 0));
        Vector3 fwd5 = transform.TransformDirection(Vector3.forward + new Vector3(-.5f, 0, 0));

        Debug.DrawRay(transform.position, fwd, Color.red, 5);
        Debug.DrawRay(transform.position, fwd2, Color.red, 5);
        Debug.DrawRay(transform.position, fwd3, Color.red, 5);
        Debug.DrawRay(transform.position, fwd4, Color.red, 5);
        Debug.DrawRay(transform.position, fwd5, Color.red, 5);


        if (Physics.Raycast(transform.position, fwd, out hit, 2)
            || Physics.Raycast(transform.position, fwd2, out hit, 2)
            || Physics.Raycast(transform.position, fwd3, out hit, 2)
            || Physics.Raycast(transform.position, fwd4, out hit, 2)
            || Physics.Raycast(transform.position, fwd5, out hit, 2))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
