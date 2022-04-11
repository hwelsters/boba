using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : BaseItemUI
{
    // FOR TESTING
    private void Start()
    {
        SetupUI();
    }

    private void OnEnable()
    {
        Inventory.OnChange += SetupUI;
    }

    private void OnDisable()
    {
        Inventory.OnChange += SetupUI;
    }

    // REDO REDO REDO REDO REDO
    // This is literally a huge chunk of spaghetti code right now so I will have to untangle this mess sometime
    // HAS A GREAT AMOUNT OF OVERLAP WITH ShopUI, consider using functional programming to shorten code and make it more readable
    public void SetupUI()
    {
        Globals.DestroyAllTagged("InventoryItemUI");
        List<Item> items = Inventory.GetItems();
        for (int i = 0; i < items.Count; i++)
        {
            Vector2 instantiatePosition = INITIAL_UI_POSITION + new Vector2((i % 3) * UI_WIDTH, -(i / 3) * UI_HEIGHT);
            GameObject instantiatedUI = Instantiate(itemUIObject, Vector2.zero, Quaternion.identity);
            RectTransform instantiatedUIRect = instantiatedUI.GetComponent<RectTransform>();
            Image instantiateUISpriteImage = instantiatedUI.GetComponent<Image>();
            Text itemCountText = instantiatedUI.GetComponentInChildren<Text>();
            Sprite itemSprite = Globals.GetItemInfo(items[i].itemType).sprite;

            // A copy has to be made for lambda function to function properly
            int tempI = i;

            // Setup UI Object
            instantiatedUIRect.SetParent(transform);
            instantiatedUIRect.anchoredPosition = instantiatePosition;
            instantiateUISpriteImage.sprite = itemSprite;
            itemCountText.text = items[i].count.ToString();
        }
    }
}
