using System;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Sprite merchantPortrait;
    [SerializeField] private List<ItemType> itemsForSale;
    [SerializeField] [TextArea] private string shopDescription; 

    public void Buy (int soldItemIndex, int amount)
    {
        Debug.Log(soldItemIndex);
        ItemType soldItemType = this.itemsForSale[soldItemIndex];
        
        ItemInfo soldItemInfo = Globals.GetItemInfo(soldItemType);

        // Count the greatest quantity the player can buy
        int maxAmount = Inventory.GetMaxPurchasable(soldItemInfo.price);
        amount = Math.Min(amount, maxAmount);

        int itemsCost = amount * soldItemInfo.price;

        if (Inventory.WithdrawMoney(itemsCost))
            Inventory.Store(new Item(soldItemType, amount, Globals.ROOM_TEMPERATURE));
    }

    public List<ItemType> GetItemsForSale()
    {
        return this.itemsForSale;
    }

    public Sprite GetMerchantPortrait()
    {
        return this.merchantPortrait;
    }

    public string GetShopDescription()
    {
        return this.shopDescription;
    }
}
