using System;

[Serializable]
public class Item
{
    public ItemType itemType;

    public int count;

    public float temperatureInKelvin = 0f;

    public Item(ItemType itemType, int count, float temperatureInKelvin)
    {
        this.itemType = itemType;
        this.count = count;
        this.temperatureInKelvin = temperatureInKelvin;
    }

    public void Store(Item item, int maxCount)
    {
        if (item.GetItemType() == this.GetItemType() || this.GetItemType() == ItemType.NONE)
        {
            int currAmount = this.GetCount() + item.GetCount();
            int tempCount = this.GetCount();

            this.SetCount( currAmount > maxCount? maxCount: currAmount );
            this.CombineTemperature(this.GetCount() - tempCount, tempCount, item.temperatureInKelvin);

            item.SetCount(currAmount - this.GetCount());

            this.SetItemType( item.GetItemType() );

            item.OnEmpty();
            OnEmpty();
        }
    }

    public void Withdraw(Item item, int amount)
    {
        if (this.count >= amount && (item.itemType == this.itemType || item.itemType == ItemType.NONE))
        {
            //DELETE
            item.CombineTemperature(amount, item.count, this.temperatureInKelvin);

            item.SetCount( (int)(item.GetCount() + amount) );

            this.SetCount( (int)(this.GetCount() - amount) );
        
            item.SetItemType( this.itemType );

            item.OnEmpty();
            OnEmpty();
        }
    }

    public void Reset()
    {
        this.SetCount(0);
        this.OnEmpty();
    }

    public bool Combine(Item item)
    {
        string combineKey = Globals.ConvertToKey( new ItemType[] {item.itemType, this.itemType} );

        if (Globals.instance.itemCombinations.ContainsKey(combineKey)) 
        {
            ItemType combinedItem = Globals.instance.itemCombinations[combineKey];
            this.SetItemType(combinedItem);  

            this.CombineTemperature(1, item.GetCount(), item.GetTemperature());

            item.Reset();   
            
            return true;
        }

        return false;
    }   

    //DELETE
    public void CombineTemperature(float amountAdded, float tempCount, float temperature)
    {
        float totalTemperature = (tempCount * this.GetTemperature()) + (amountAdded * temperature);
        float averageTemperature = (totalTemperature) / (amountAdded + tempCount);

        this.temperatureInKelvin = averageTemperature;
    }

    public void AddTemperature(float amount, float temperatureToApproach)
    {
        if (this.count > 0)
        {
            ItemInfo itemInfo = Globals.GetItemInfo(this.itemType);

            this.SetTemperature(UnityEngine.Mathf.MoveTowards(this.GetTemperature(), temperatureToApproach, amount));
            
            if (this.GetTemperature() > itemInfo.meltTemperature)
            {
                if (itemInfo.meltItemType != ItemType.NONE)
                {
                    this.SetItemType(itemInfo.meltItemType);
                }
            }
            if (this.GetTemperature() < itemInfo.freezeTemperature)
            {
                if (itemInfo.freezeItemType != ItemType.NONE)
                {
                    this.SetItemType(itemInfo.freezeItemType);
                }
            }
        }
    }

    public void OnEmpty()
    {
        if (this.count <= 0) 
        {
            this.itemType = ItemType.NONE;
            this.temperatureInKelvin = 0;
        }
    }

    public ItemType GetItemType() { return this.itemType; }
    public void SetItemType( ItemType itemType ) { this.itemType = itemType; }

    public int GetCount() { return this.count; }
    public void SetCount( int count ) { this.count = count; }

    public float GetTemperature() { return this.temperatureInKelvin; }
    private void SetTemperature( float temperatureInKelvin ) { this.temperatureInKelvin = temperatureInKelvin; }

    // HELPER METHODS FOR DEBUGGING
    public override string ToString()
    {
        return "ItemType: " + this.itemType + " Count: " + this.count;
    }
}