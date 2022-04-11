using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Storage
{
    public override void StoreItem(Item item)
    {
        item.Reset();
    }
}
