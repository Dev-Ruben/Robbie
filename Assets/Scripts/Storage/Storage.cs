using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class Storage
{
    protected int id;
    protected StoredItem[] storageSlots;
    private StorageTypes type;

    public void SetID(int sID)
    {
        id = sID;
    }
    public void Create(int size, StorageTypes sType)
    {
        storageSlots = new StoredItem[size];
        type = sType;
        for (int i = 0; i < size; i++)
        {
            storageSlots[i] = new StoredItem();
            storageSlots[i].SetSlotID(i);
            storageSlots[i].EmptySlot();
        }
    }

    //Add HoldingItem, or give itemData to put in Storage.
    public bool Add(ItemData itemData = null, int itemAmount = 0, int slotID = -1, bool swap = false)
    {
        HoldingItem holdingItem = StorageManager.Instance.GetHoldingItem();
        Storage oldStorage = null;
        StoredItem oldStorageSlot = null;
        if (!holdingItem.IsEmpty())
        {
            oldStorage = holdingItem.GetOldStorage();
            oldStorageSlot = oldStorage.GetStoredItem(holdingItem.GetOldSlotID());
        }
        


        if (itemData == null)
        {
            itemData = holdingItem.GetItemData();
        }
        if (itemAmount == 0)
        {
            itemAmount = holdingItem.GetAmount();
        }

        bool placeItem = StorageManager.Instance.CheckItemType(itemData, type, slotID);

        //Add Check for if item is stackable
        if (placeItem)
        {
            if(slotID == -1)
            {
                if(Array.Exists(storageSlots, item => item.GetID() == itemData.id) && itemData.stackable)
                {
                    StoredItem storedItem = storageSlots.First(item => item.GetID() == itemData.id);
                    Increase(storedItem, itemAmount);
                }
                else if(Array.Exists(storageSlots, item => item.GetID() == -1))
                {
                    StoredItem storedItem = storageSlots.First(item => item.GetID() == -1);
                    storedItem.StoreItem(itemData, itemAmount);
                }    
                else
                {
                    return false;
                }  
            }
            else
            {
                if (storageSlots[slotID].GetID() == itemData.id && itemData.stackable)
                {
                    Increase(storageSlots[slotID], itemAmount);
                }
                else if (storageSlots[slotID].GetID() == -1)
                {
                    storageSlots[slotID].StoreItem(itemData, itemAmount);
                }
                else if (!holdingItem.IsEmpty())
                {
                    if((oldStorageSlot.GetID() == -1 || (oldStorageSlot.GetID() == storageSlots[slotID].GetItemData().id && storageSlots[slotID].GetItemData().stackable)) && StorageManager.Instance.CheckItemType(storageSlots[slotID].GetItemData(), oldStorage.Type(), holdingItem.GetOldSlotID()))
                    {
                        oldStorage.Add(storageSlots[slotID].GetItemData(), storageSlots[slotID].GetStackSize(), holdingItem.GetOldSlotID(), true);
                        storageSlots[slotID].StoreItem(itemData, itemAmount);
                        
                    }
                    else
                    {
                        oldStorage.Add(itemData, itemAmount, holdingItem.GetOldSlotID(), true);
                    }
                }
            }            
        }
        else if (!holdingItem.IsEmpty() && !swap)
        {
            oldStorage.Add(itemData, itemAmount, holdingItem.GetOldSlotID(), true);
        }
        else return false;

        if(!holdingItem.IsEmpty() && !swap)
            holdingItem.SetAmount(holdingItem.GetAmount() - itemAmount);
        
        StorageManager.Instance.UpdateStorage(id,type);
        if (!holdingItem.IsEmpty())
        {
            StorageManager.Instance.UpdateStorage(oldStorage.ID(),oldStorage.type);
        }  
        return true;

    }
    
    public void Remove(int slotID, int itemAmount)
    {
        Decrease(storageSlots[slotID], itemAmount);
    }

    //Slot ID, Item ID, item Amount
    private void Increase(StoredItem storedItem, int itemAmount)
    {
        storedItem.IncreaseStack(itemAmount);    
    }
    private void Decrease(StoredItem storedItem, int itemAmount)
    {
        storedItem.DecreaseStack(itemAmount);    
    }

    public int ID()
    {
        return id;
    }
    
    public StoredItem[] StoredItems()
    {
        return storageSlots;
    }
    public StoredItem GetStoredItem(int slotID)
    {
        return storageSlots[slotID];
    }

    public StorageTypes Type()
    {
        return type;
    }

}
 public enum StorageTypes
    {
        None,
        Bag,
        Hotbar,
        Armory,
        Chest,
        Shop,
    }
