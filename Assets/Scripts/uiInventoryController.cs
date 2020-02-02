using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiInventoryController : MonoBehaviour
{
    public Sprite woodSprite;
    public Sprite stoneSprite;
    public Sprite cowSpraySprite;
    public Sprite toolSprite;

    public GameObject player;
    private playerInventory inventory;

    private List<Image> inventorySlots = new List<Image>();

    void Awake()
    {
        inventory = player.GetComponent<playerInventory>();
        for(int i = 0; i < transform.childCount; i++)
        {
            var childImage = transform.GetChild(i);
            Debug.Log(childImage + " " + childImage.name);
            if (childImage.name.StartsWith("InventorySlot")) {
                inventorySlots.Add(childImage.GetComponent<Image>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            string item = inventory.getItemInSlot(i);
            inventorySlots[i].sprite = GetSpriteForItem(item);
        }
    }

    public Sprite GetSpriteForItem(string item) {
        switch(item) {
            case "wood": return woodSprite;
            case "stone": return stoneSprite;
            case "cowspray": return cowSpraySprite;
            case "tool": return toolSprite;
            default: return null;
        }
    }
}
