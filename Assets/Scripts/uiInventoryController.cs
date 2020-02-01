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

            if (item == "wood")
                childImage.GetComponent<Image>().sprite = woodSprite;
            else if (item == "stone")
                childImage.GetComponent<Image>().sprite = stoneSprite;
            else if (item == "cowspray")
                childImage.GetComponent<Image>().sprite = cowSpraySprite;
            else if (item == "tool")
                childImage.GetComponent<Image>().sprite = toolSprite;
            else
                childImage.GetComponent<Image>().sprite = null;
        }
    }
}
