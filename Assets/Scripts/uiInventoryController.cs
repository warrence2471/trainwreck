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
        for(int i = 0; i < transform.childCount; i++)
        {
            var item = inventory.getItemInSlot(i);
            var childImage = transform.GetChild(i);

            if (item == "wood")
                childImage.GetComponent<Image>().sprite = woodSprite;
            if (item == "stone")
                childImage.GetComponent<Image>().sprite = stoneSprite;
            if (item == "cowspray")
                childImage.GetComponent<Image>().sprite = cowSpraySprite;
            if (item == "tool")
                childImage.GetComponent<Image>().sprite = toolSprite;
        }
    }
}
