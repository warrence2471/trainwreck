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

    void Update()
    {
        int inventorySlot = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1))
            inventorySlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            inventorySlot = 1;
        else
            return;

        GameObject targetItem = null;
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 2, Color.green);
        if (Physics.Raycast(transform.position, fwd, out hit, 2))
        {
            targetItem = hit.collider.gameObject;
        }

        if (!targetItem) return;

        if (targetItem.tag == "pickup")
        {
            addItem(targetItem.GetComponent<pickupController>().itemName, inventorySlot);
            Destroy(targetItem.gameObject);
        }
        else if (targetItem.tag == "repair")
        {
            repairController repairC = targetItem.GetComponent<repairController>();
            string neededItem = repairC.itemName;
            Debug.Log("Trying to repair " + targetItem.name + " now - need " + neededItem + " to do so.");

            if (hasItem(neededItem)) {
                Debug.Log("Item found!");
                useItem(neededItem);
                repairC.Trigger();
                // Destroy(targetItem.gameObject);
            } else {
                Debug.Log("Do not have the item");
            }
        }
    }
}
