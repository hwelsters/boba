using UnityEngine;

public class Storage : Interactable
{
    [Header("Storage class")]
    //questionable naming
    public Item storedItem;
    public Item uiItem;

    [SerializeField]
    protected StorageType storageType;
    
    [Header("Carried object")]
    [SerializeField] public GameObject carriedObject;

    private const int CARRIED_OBJECT_SIZE = 3;

    protected virtual void Start()
    {
        UpdateUI();
    }

    public virtual void WithdrawItem(Item item)
    {
        storedItem.Withdraw(item, 1);
    }

    public virtual void StoreItem(Item item)
    {
        if (!(storedItem.count == 1 && storedItem.Combine(item)))
            storedItem.Store(item, storageType.maxCount);
    }

    protected virtual void FixedUpdate()
    {
        if (uiItem.itemType != storedItem.itemType || uiItem.count != storedItem.count)
        {
            uiItem.itemType = storedItem.itemType;
            uiItem.count = storedItem.count;
            UpdateUI();
        }
        storedItem.AddTemperature(storageType.temperatureChangeRate * Time.deltaTime, storageType.temperatureToApproach);
    }

    //CALCULATES WHERE WE START CREATING UI STUFF
    protected float CalculateStart(int amount)
    {
        amount = Mathf.Min(amount, 3);
        return -(amount - 1)* CARRIED_OBJECT_SIZE * 0.5f;
    }

    //THIS FUNCTION IS AWFULLY SPECIFIC
    //IF THERE IS A FUNCTION THAT IS SIMILAR, IT WILL BE WISE TO MAKE IT BETTER
    //MY CODE IS FUCKING DUMB RIGHT NOW
    protected void UpdateUI()
    {
        DestroyChildren();

        float startPosition = CalculateStart(storedItem.count);
        for (int i = 0; i < storedItem.count; i++)
        {
            float xPosition = startPosition + (i % 3 * CARRIED_OBJECT_SIZE);
            int yPosition = i / 3 * CARRIED_OBJECT_SIZE;
            Vector2 instantiatePosition = (Vector2)transform.position + new Vector2(xPosition, yPosition) + storageType.offset;

            GameObject instantiatedObject = Instantiate(carriedObject, instantiatePosition, Quaternion.identity);
            
            SpriteRenderer spriteRenderer = instantiatedObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Globals.GetItemInfo(storedItem.itemType).sprite;

            instantiatedObject.transform.SetParent(gameObject.transform);
        }
    }

    //MIGHT PUT IT IN A PARENT CLASS
    protected void DestroyChildren()
    {
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}