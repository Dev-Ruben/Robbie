using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredItem
{
    private int slotID;
    private ItemData itemData;
    private int stackSize;

    public void SetSlotID(int id)
    {
        slotID = id;
    }

    public void EmptySlot()
    {
        itemData = null;
        stackSize = 0;

    }

    public void StoreItem(ItemData data, int amount)
    {
        itemData = data;
        stackSize = amount;

    }
    public void IncreaseStack(int amount)
    {
        stackSize += amount;
    }
    public void DecreaseStack(int amount)
    {
        stackSize -= amount;
        if (stackSize <= 0)
        {
            EmptySlot();
        }
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    public int GetSlotID()
    {
        return slotID;
    }
    public int GetID() {
        if (itemData) { 
            return itemData.id;
        }

        return -1;
    }

    public int GetStackSize()
    {
        return stackSize;
    }

    public bool IsEmpty()
    {
        if (itemData != null){
            return false;
        }
        else{
            return true;
        }
    }
}


