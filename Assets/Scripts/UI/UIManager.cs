using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    UI ui;  
    CustomCursor customCursor;
    InventoryState inventoryState;
    List<Slot> allSlots;

    protected override void Awake()
    {
        base.Awake();
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        customCursor = ui.GetCustomCursor().GetComponent<CustomCursor>();
        allSlots = ui.GetAllSlots();
    }

    public InventoryState GetInventoryState()
    {
        return inventoryState;
    }

    public void ToggleInventory(int playerID)
    {
        bool state = SwitchInventoryActiveState();
        ToggleBag(state);
        ToggleArmory(state);
        ResizeInventoryUI(state);
            
        if (!state)
        {
            SetPanel(false);
            inventoryState = InventoryState.Closed;

            if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
            {
                StorageManager.Instance.GetHoldingItem().GetOldStorage().Add(StorageManager.Instance.GetHoldingItem().GetItemData(), StorageManager.Instance.GetHoldingItem().GetAmount());
            }
        }
        else
        {
            inventoryState = InventoryState.Open;
            ui.bag.transform.position = new Vector2(1080,600);
            ui.armory.transform.position = new Vector2(250,600);
        }
        UpdatePlayerSlots(playerID);
    }

    private void ToggleBag(bool state)
    {
        ui.bag.SetActive(state);
    }
    private void ToggleArmory(bool state)
    {
        ui.armory.SetActive(state);
    }
    public bool ToggleChest(int chestID, int playerID)
    {
        bool state = SwitchInventoryActiveState();
        ui.GetChest().SetActive(state);
        ToggleBag(state);

        GameManager.Instance.getPlayer().GetComponent<PlayerAction>().SetCurrentActionID(chestID);

         if (!state)
        {
            SetPanel(false);
            inventoryState = InventoryState.Closed;

            if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
            {
                StorageManager.Instance.GetHoldingItem().GetOldStorage().Add(StorageManager.Instance.GetHoldingItem().GetItemData(), StorageManager.Instance.GetHoldingItem().GetAmount());
            }
        }
        else
        {
            inventoryState = InventoryState.Open;
            ui.bag.transform.position = new Vector2(1300,600);
            ui.GetChest().transform.position = new Vector2(300,600);
        }
        GameManager.Instance.switchActionableState();

        UpdateBag(playerID);
        UpdateChest(chestID);
        return state;
    }

    public bool ToggleShop(int shopID, int playerID)
    {
        bool state = SwitchInventoryActiveState();
        ui.shop.SetActive(state);
        ToggleBag(state);

        GameManager.Instance.getPlayer().GetComponent<PlayerAction>().SetCurrentActionID(shopID);

        if (!state)
        {
            SetPanel(false);
            inventoryState = InventoryState.Closed;

            if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
            {
                StorageManager.Instance.GetHoldingItem().GetOldStorage().Add(StorageManager.Instance.GetHoldingItem().GetItemData(), StorageManager.Instance.GetHoldingItem().GetAmount());
            }
        }
        else
        {
            inventoryState = InventoryState.Open;
            ui.bag.transform.position = new Vector2(1300,600);
            ui.shop.transform.position = new Vector2(300,600);
        }
        GameManager.Instance.switchActionableState();

        UpdateBag(playerID);
        UpdateShop(shopID);
        return state;
    }

     public void UpdatePlayerSlots(int playerID)
    {
        UpdateBag(playerID);
        UpdateArmory(playerID);
        UpdateHotbar(playerID);
        UpdateBalance(playerID);
    }

    public void UpdateBag(int playerID)
    {
        Slot[] bagSlots = ui.GetBagSlots();
        Storage bagStorage = StorageManager.Instance.GetStorage(playerID, StorageTypes.Bag);
        UpdateSlots(bagSlots,bagStorage);
    }

    public void UpdateArmory(int playerID)
    {
        Slot[] armorySlots = ui.GetArmorySlots();
        Storage armoryStorage = StorageManager.Instance.GetStorage(playerID, StorageTypes.Armory);
        GameObject.FindWithTag("Player").GetComponent<Equipment>().Equip();//Check for all equip types
        UpdateSlots(armorySlots,armoryStorage);
    }
    public void UpdateChest(int chestID)
    {
        Slot[] chestSlots = ui.GetChestSlots();
        Storage chestStorage = StorageManager.Instance.GetStorage(chestID, StorageTypes.Chest);
        UpdateSlots(chestSlots,chestStorage);
    }

    public void UpdateHotbar(int playerID)
    {
        Slot[] hotbarSlots = ui.GetHotbarSlots();
        Storage hotbarStorage = StorageManager.Instance.GetStorage(playerID, StorageTypes.Hotbar);
        UpdateSlots(hotbarSlots,hotbarStorage);
    }

    public void UpdateShop(int shopID)
    {
        Slot[] shopSlots = ui.GetShopSlots();
        Storage shopStorage = StorageManager.Instance.GetStorage(shopID, StorageTypes.Shop);
        GameManager.Instance.GetNpc(GameManager.Instance.getPlayer().GetComponent<PlayerAction>().GetCurrentActionID()).GetComponent<Merchant>().UpdateMerchant();
        
        UpdateSlots(shopSlots,shopStorage);
    }
    public void UpdateBalance(int balanceID)
    {
        ui.GetPouch().GetComponentInChildren<TextMeshProUGUI>().text = "$ " + StorageManager.Instance.GetBalance(balanceID).GetBalance().ToString();
    }

    public void UpdateSlots(Slot[] slots, Storage storage)
    {
        StoredItem[] storedItems = storage.StoredItems();

        foreach (Slot slot in slots)
        {
            if (storedItems[slot.id].GetItemData() != null)
            {
                slot.Fill(storedItems[slot.id]);
            }
            else
            {
                slot.Empty();
            }
        }
    }      
    public bool SwitchInventoryActiveState()
    {
        bool state = !ui.inventory.activeSelf;
        ui.inventory.SetActive(state); 
        return state;
    }
    public void SetPanel(bool state, ItemData itemData = null)
    {
        GameObject panel = ui.GetInfoPanel();
        if (state == true)
            panel.GetComponent<InfoPanel>().SetInfo(itemData);
        panel.transform.position = Input.mousePosition;
        panel.gameObject.SetActive(state);
    }
    public void FlipPanel()
    {
        GameObject bottomPanel = ui.GetInfoPanel().GetComponent<InfoPanel>().bottomPanel;
    }

    public CustomCursor GetCustomCursor()
    {
        return customCursor;
    }
 
    public void ResizeInventoryUI(bool isOpen)
    {
            if (isOpen)
            {
                    ui.GetStatsBar().transform.localScale = new Vector3(0.8f,0.8f,0.8f);
                    ui.GetMiniMap().transform.localScale = new Vector3(0.8f,0.8f,0.8f);
                    ui.GetHotbar().transform.localScale = new Vector3(1.2f,1.2f,1.2f);
                    ui.GetWeaponSlot().transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            }
            if (!isOpen)
            {
                    ui.GetStatsBar().transform.localScale = new Vector3(1f,1f,1f);
                    ui.GetMiniMap().transform.localScale = new Vector3(1f,1f,1f);
                    ui.GetHotbar().transform.localScale = new Vector3(1f,1f,1f);
                    ui.GetWeaponSlot().transform.localScale = new Vector3(1f,1f,1f);

            }
    }
    ///### Move out of UIManager

    public List<Slot> GetSlotsList()
    {
        return ui.GetAllSlots();
    }
}
    ////###
/*
    public void PlaceItem(Storage newStorage = null, int newSlotID = -1,int placeAmount = 0)
    {
        Storage oldStorage = null;

        ItemData itemData = null;

        StorageTypes newSlotType = StorageTypes.None;
        StorageTypes oldSlotType = StorageTypes.None;

        int oldSlotID;

        bool placeItem = false;

        if (newSlotID == -1 && newStorage == null)
        { 
            Slot slot = CheckForNearestSlot();
            newSlotID = slot.id;
            newSlotType = slot.type;
            if (newSlotType == StorageTypes.Chest)
                newStorage = StorageManager.Instance.GetStorage(currentActionID,newSlotType);
            else
                newStorage = StorageManager.Instance.GetStorage(0,newSlotType);
        }
        else
        {
            newSlotType = newStorage.Type();
        }

        //set hold item to place item
        itemData = StorageManager.Instance.GetHoldingItem().GetItemData();
        if (placeAmount == 0)
            placeAmount = StorageManager.Instance.GetHoldingItem().GetAmount();

        oldSlotID = StorageManager.Instance.GetHoldingItem().GetOldSlotID();
        oldSlotType = StorageManager.Instance.GetHoldingItem().GetOldSlotType();
        if (oldSlotType == StorageTypes.Chest)
            oldStorage = StorageManager.Instance.GetStorage(currentActionID,oldSlotType);
        else
            oldStorage = StorageManager.Instance.GetStorage(0,oldSlotType);

        placeItem = CheckItemType(itemData, newSlotType, newSlotID);
         
        if (placeItem)
        {
            if (newStorage.GetStoredItem(newSlotID).GetItemData() == null || newStorage.GetStoredItem(newSlotID).GetID() == itemData.id && itemData.stackable)
            {
                //place item
                newStorage.Add(itemData, placeAmount,newSlotID);
            }
            else
            {
                if(oldStorage.GetStoredItem(oldSlotID).GetID() == -1 && CheckItemType(newStorage.GetStoredItem(newSlotID).GetItemData(), oldSlotType, oldSlotID))
                {

                    //place item & put nearestslot item to old slot
                    oldStorage.Add(newStorage.GetStoredItem(newSlotID).GetItemData(), newStorage.GetStoredItem(newSlotID).GetStackSize(),oldSlotID);
                    newStorage.Add(itemData, placeAmount,newSlotID);
                }
                else
                {
                    //place holditem in old slot
                    oldStorage.Add(itemData, placeAmount,oldSlotID);
                }
            }
        }
        else
        {
            oldStorage.Add(itemData, placeAmount, oldSlotID);
        }

        if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
        {
            StorageManager.Instance.GetHoldingItem().SetItemAmount(StorageManager.Instance.GetHoldingItem().GetAmount() - placeAmount);

            if (StorageManager.Instance.GetHoldingItem().GetAmount() < 1)
                customCursor.SetCustomCursor();
        }
        UpdatePlayerSlots(0);
        if (newStorage.Type() == StorageTypes.Chest)
        {
            UpdateChest(newStorage.ID());
        }   
        if(oldStorage.Type() == StorageTypes.Chest)
        {
             UpdateChest(oldStorage.ID());
        }
    }
*/
   
