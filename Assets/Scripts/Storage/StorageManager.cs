using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class StorageManager : SingletonMonobehaviour<StorageManager>
{
    private List<Storage> bagList;
    private List<Storage> hotbarList;
    private List<Storage> armoryList;
    private List<Storage> chestList;
    private List<Storage> shopList;

    private  HoldingItem holdingItem;
    private List<Balance> balances;

    protected override void Awake()
    {
        base.Awake();
        bagList = new List<Storage>();
        hotbarList = new List<Storage>();
        armoryList = new List<Storage>();
        chestList = new List<Storage>();
        shopList = new List<Storage>();

        balances = new List<Balance>();

        holdingItem = GetComponent<HoldingItem>();

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        int playerID = player.GetComponent<ID>().GetID();

        Storage bag = new Storage();
        Storage hotbar = new Storage();
        Storage armory = new Storage();
        Balance balance = new Balance();

        bag.SetID(playerID);
        hotbar.SetID(playerID);
        armory.SetID(playerID);
        balance.SetID(playerID);

        bag.Create((int)StorageSize.InventorySize , StorageTypes.Bag);
        hotbar.Create((int)StorageSize.HotbarSize, StorageTypes.Hotbar);
        armory.Create((int)StorageSize.ArmorySize, StorageTypes.Armory);
        balance.Create();

        bagList.Add(bag);
        hotbarList.Add(hotbar);
        armoryList.Add(armory);
        balances.Add(balance);
    }
    public int CreateChest()
    {
        Storage chest = new Storage();
        chest.SetID(chestList.Count);

        chest.Create((int)StorageSize.ChestSize, StorageTypes.Chest);

        chestList.Add(chest);
        return chest.ID();
    }

    public int CreateShop(int id)
    {
        Storage shop = new Storage();
        shop.SetID(id);

        shop.Create(4, StorageTypes.Shop);

        shopList.Add(shop);
        return shop.ID();
    }

    public Storage GetStorage(int ID, StorageTypes type)
    {
        Storage storage = new Storage();
        if (type == StorageTypes.Bag)
        {
            storage = bagList[ID];
        }
        if (type == StorageTypes.Hotbar)
        {
            storage = hotbarList[ID];
        }
        if (type == StorageTypes.Armory)
        {
            storage = armoryList[ID];
        }
        if (type == StorageTypes.Chest)
        {
            storage = chestList[ID];
        }
        if (type == StorageTypes.Shop)
        {
            storage = shopList[ID];
        }
        return storage;
    }

    public Balance GetBalance(int id)
    {
        return balances[id];
    }

    public List<Storage> GetStorageList(StorageTypes type)
    {
        if (type == StorageTypes.Bag)
        {
            return bagList;
        }
        if (type == StorageTypes.Hotbar)
        {
            return hotbarList;
        }
        if (type == StorageTypes.Armory)
        {
            return armoryList;
        }
        if (type == StorageTypes.Chest)
        {
            return chestList;
        }
        if (type == StorageTypes.Shop)
        {
            return shopList;
        }
        return null;
    }

    public Storage FindStorage()
    {
        Slot slot = CheckForNearestSlot();

        StorageTypes newSlotType = slot.type;

        if (newSlotType == StorageTypes.Chest)
            return StorageManager.Instance.GetStorage(GameManager.Instance.getPlayer().GetComponent<PlayerAction>().GetCurrentActionID(),newSlotType);
        else
            return StorageManager.Instance.GetStorage(0,newSlotType);
    }

    public Slot FindSlot()
    {
        return CheckForNearestSlot();
    }

    private Slot CheckForNearestSlot()
    {
        Slot nearestSlot = null;
        float shortestDistance = float.MaxValue;

        foreach (Slot slot in UIManager.Instance.GetSlotsList())  //Change
        {
            if (slot.isActiveAndEnabled)
            {
                float distance = Vector2.Distance(Input.mousePosition, slot.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestSlot = slot;
                }
            }
        }
        return nearestSlot;
    }

    public bool CheckItemType(ItemData itemData, StorageTypes slotType, int slotID)
    {
        if(slotType == StorageTypes.Bag)
        {
            return true;
        }
        if(slotType == StorageTypes.Armory)
        {
            if (itemData.type == ItemType.Weapon && slotID > 3 && slotID != -1)
            {
                return true;
            }
            if (itemData.type == ItemType.Armor && slotID < 4 && slotID != -1)
            {
                if (itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Helmet && slotID == 0){
                    return true;
                }
                if (itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Body && slotID == 1){
                    return true;
                }
                if (itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Leggins && slotID == 2){
                    return true;
                }
                if (itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Boots && slotID == 3){
                    return true;
                }
            }
        }
        if(slotType == StorageTypes.Hotbar)
        {
            return true;
        }
        if(slotType == StorageTypes.Chest)
        {
            return true;
        }
        if(slotType == StorageTypes.Shop)
        {
            ShopSlot[] shopSlots = GameObject.Find("Canvas").GetComponent<UI>().GetShopSlots();
            if (shopSlots[slotID].slotType == SlotType.Buy)
                return false;
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public Storage GetOldStorage()
    {
        return holdingItem.GetOldStorage();
    }
    public HoldingItem GetHoldingItem()
    {
        return holdingItem;
    }

    public void UpdateStorage(int id, StorageTypes type)
    {
        if(type == StorageTypes.Bag)
        {
            UIManager.Instance.UpdateBag(id);
        }
        if(type == StorageTypes.Armory)
        {
            UIManager.Instance.UpdateArmory(id);
        }
        if(type == StorageTypes.Hotbar)
        {
            UIManager.Instance.UpdateHotbar(id);
        }
        if(type == StorageTypes.Chest)
        {
            UIManager.Instance.UpdateChest(id);
        }
        if(type == StorageTypes.Shop)
        {
            UIManager.Instance.UpdateShop(id);
        }
    }

    //Refactor V
    public void HoldItem(Slot slot, bool halfStack = false)
    {
        Storage storage;
        if (slot.type == StorageTypes.Chest)
            storage = StorageManager.Instance.GetStorage(GameManager.Instance.getPlayer().GetComponent<PlayerAction>().GetCurrentActionID(),slot.type);
        else
            storage = StorageManager.Instance.GetStorage(0,slot.type);

        StoredItem[] storedItems = storage.StoredItems();
        int stackAmount;
        if (halfStack && slot.amount > 1)
        {
            stackAmount = (int)Math.Ceiling((float)slot.amount / 2);
        }
        else
        {
            stackAmount = slot.amount;
        }

        StorageManager.Instance.GetHoldingItem().HoldItem(slot.itemData, stackAmount, storage, slot.GetID());
        //UIManager.Instance.GetCustomCursor().SetCustomCursor(slot.itemImage);
        UIManager.Instance.SetPanel(false);
        
        storage.Remove(slot.id, stackAmount);

        UpdateStorage(storage.ID(), storage.Type()); 
    }
}

 /*### TEST CODE
    public void HoldItemTEST(Storage storage, int slotID, int stackSize = 0)
    {
        StoredItem item = storage.GetStoredItem(slotID);

        if (stackSize == 0)
        {
            stackSize = item.GetStackSize();
        }
        //customCursor.SetCustomCursorTEST(item.GetItemData().display, item.GetItemData(), stackSize, storage, slotID);
        
        storage.Remove(slotID, stackSize);

        SetPanel(false);

        UpdatePlayerSlots(0);
        if (storage.Type() == StorageTypes.Chest)
        {
            UpdateChest(storage.ID());
        }
    }*/