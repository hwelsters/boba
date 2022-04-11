using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : BaseItemUI
{
    public Shop activeShop = null;
    public int buyQuantity = 1;

    [SerializeField] private Image merchantPortraitImage;
    [SerializeField] private Text descriptionText;

    private void Start()
    {
        // ONLY FOR TESTING, WILL BE REMOVED
        SetupUI(); 
    }

    public void BuyItem(int soldItemIndex) 
    {
        activeShop?.Buy(soldItemIndex, buyQuantity);
    }

    // REDO REDO REDO REDO REDO
    // This is literally a huge chunk of spaghetti code right now so I will have to untangle this mess sometime
    // HAS A GREAT AMOUNT OF OVERLAP WITH InventoryUI, consider using functional programming to shorten code and make it more readable
    public void SetupUI()
    {
        Globals.DestroyAllTagged("ShopItemUI");
        SetShopKeeperSprite();
        SetShopDescription();
        List<ItemType> itemsForSale = activeShop.GetItemsForSale();
        for (int i = 0; i < itemsForSale.Count; i++)
        {
            Vector2 instantiatePosition = INITIAL_UI_POSITION + new Vector2((i % 3) * UI_WIDTH, -(i / 3) * UI_HEIGHT);
            GameObject instantiatedUI = Instantiate(itemUIObject, Vector2.zero, Quaternion.identity);
            RectTransform instantiatedUIRect = instantiatedUI.GetComponent<RectTransform>();
            Button instantiatedUIButton = instantiatedUI.GetComponent<Button>();
            Image instantiateUISpriteImage = instantiatedUI.GetComponent<Image>();
            Sprite itemSprite = Globals.GetItemInfo(itemsForSale[i]).sprite;

            // A copy has to be made for lambda function to function properly
            int tempI = i;

            // Setup UI Object
            instantiatedUIRect.SetParent(transform);
            instantiatedUIRect.anchoredPosition = instantiatePosition;
            instantiateUISpriteImage.sprite = itemSprite;
            instantiatedUIButton.onClick.AddListener(()=>{ BuyItem(tempI); });
        }
    }

    public void SetShopKeeperSprite()
    {
        this.merchantPortraitImage.sprite = activeShop?.GetMerchantPortrait();
    }

    public void SetShopDescription()
    {
        this.descriptionText.text = activeShop?.GetShopDescription();
    }
}
