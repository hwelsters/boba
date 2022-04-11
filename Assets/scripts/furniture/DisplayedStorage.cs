using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayedStorage : Storage
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    // THIS IS A QUICK FIX I SHOULD REMOVE THIS LATER
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateSprite();
    }

    public override void StoreItem(Item item)
    {
        base.StoreItem(item);
        UpdateSprite();
    }

    public override void WithdrawItem(Item item)
    {
        base.WithdrawItem(item);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = base.storageType.sprites[(int) base.storedItem.itemType];
    }
}
