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
    public int ownSlot = 0;

    private playerInventory inventory;


    void Awake()
    {
        inventory = player.GetComponent<playerInventory>();
    }


    // Update is called once per frame
    void Update()
    {
        int ui_offset = 1;
        for(int i = 0; i < transform.childCount - ui_offset; i++)
        {
            var item = inventory.getItemInSlot(i);
            var childImage = transform.GetChild(ui_offset + i);

            childImage.GetComponent<Image>().sprite = GetSpriteForItem(item);
        }
    }

    Sprite GetSpriteForItem(string item) {
        switch(item) {
            case "wood": return woodSprite;
            case "stone": return stoneSprite;
            case "cowspray": return cowSpraySprite;
            case "tool": return toolSprite;
            default: return null;
        }
    }
}
