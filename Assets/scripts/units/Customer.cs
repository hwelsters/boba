using UnityEngine;

public class Customer : TrashCan
{
    //REMOVE SERIALIZEFIELD ONLY FOR TESTING
    [SerializeField]
    private ItemType drinkOrder;

    [SerializeField]
    private SpriteRenderer drinkOrderSprite;

    [SerializeField]


    protected override void Start() 
    {
        drinkOrderSprite.sprite = Globals.GetItemInfo(drinkOrder).sprite;
    }

    public override void StoreItem (Item item)
    {
        if (item.GetItemType() == drinkOrder)
        {
            UpdateUI();
            base.StoreItem(item);
        }
    }

    private interface CustomerState
    {
        public abstract void HandleAnimation();
        public abstract CustomerState Execute();
    }
}