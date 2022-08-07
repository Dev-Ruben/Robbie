using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    private ItemData itemData;
    private int amount;

    private int oldSlotID;
    private StorageTypes oldSlotType;

    private Storage oldStorage;

    void Start()
    {
        itemData = new ItemData();
        oldSlotType = StorageTypes.None;
        HoldItem();
    }

    public void HoldItem(ItemData itemData = null, int itemAmount = 0, Storage storage = null,int slotID = -1)
    {
        SetHoldItem(itemData, itemAmount);
        oldSlotID = slotID;
        oldStorage = storage;
        if (storage == null)
            oldSlotType = StorageTypes.None;
        else
            oldSlotType = storage.Type();
        if(itemData == null)
            UIManager.Instance.GetCustomCursor().SetCustomCursor();
        else
            UIManager.Instance.GetCustomCursor().SetCustomCursor(itemData.display);
            
    }

    private void SetHoldItem(ItemData data, int itemAmount)
    {
        itemData = data;
        amount = itemAmount;
    }
    private void SetOldSlot(Slot slot)
    {
        if (slot != null)
        {
            oldSlotID = slot.id;
            oldSlotType = slot.type;
        }
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    public int GetID()
    {
        return itemData.id;
    }
    public int GetAmount()
    {
        return amount;
    }
    public new ItemType GetType()
    {
        return itemData.type;
    }

    public int GetOldSlotID()
    {
        return oldSlotID;
    }
    public Storage GetOldStorage()
    {
        return oldStorage;
    }
    public StorageTypes GetOldSlotType()
    {
        return oldSlotType;
    }
    public void SetAmount(int itemAmount)
    {
        amount = itemAmount;
        if (amount < 1)
        {
            HoldItem();
        }
    }
    public bool IsEmpty()
    {
        if (!itemData)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
