using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Inventory
{
    private static List<Item> items = new List<Item>();
    private static int playerMoney = 0;

    public delegate void OnChangeHandler();
    public static event OnChangeHandler OnChange;

    // REFACTOR
    public static void Store (Item itemToStore) 
    {
        foreach (Item item in items)
        {
            if (itemToStore.GetCount() == 0) break;
            item.Store(itemToStore, 99);
        }

        if (itemToStore.GetCount() != 0)
            items.Add(itemToStore);

        // Call on change event
        OnChange();

        // FOR DEBUGGING
        PrintInventory();
    }

    public static void Withdraw (Item itemToWithdrawTo) {  }

    public static int GetMaxPurchasable (int itemCost) 
    {
        // ERROR HANDLING
        if (itemCost == 0)
        {
            Debug.Log("Item cannot cost zero!");
            return -1;
        }
        return playerMoney / itemCost;
    }

    public static bool WithdrawMoney(int amountToWithdraw)
    {
        if (amountToWithdraw > playerMoney) return false;

        Debug.Log("Player Money: " + playerMoney);
        Debug.Log("Amount to Withdraw: " + amountToWithdraw);

        playerMoney -= amountToWithdraw;
        
        Debug.Log("Player Money: " + playerMoney);
        return true;
    }

    public static bool DepositMoney(int amountToDeposit) 
    {
        playerMoney += amountToDeposit;
        return true;
    }

    // HELPER FUNCTION FOR DEBUGGING
    private static void PrintInventory()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i].ToString());
        }
    }

    public static List<Item> GetItems()
    {
        return items;
    }
}
