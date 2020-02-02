using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
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
            Repair(GetTarget());
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Pickup(GetTarget(), 0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Pickup(GetTarget(), 1);
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
            if (!hasItem(neededItem))
                return;
        }

        neededItems.ForEach(i => useItem(i));
        repairC.Trigger();
    }

    GameObject GetTarget()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 2, Color.green);
        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            return hit.collider.gameObject;
        }
        return null;
    }
}
