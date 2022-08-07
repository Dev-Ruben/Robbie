using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.EventSystems;
using System;

public class SlotInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    Slot slot;

    void Start()
    {
        slot = GetComponent<Slot>();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slot.itemData || !StorageManager.Instance.GetHoldingItem().IsEmpty())
        {
            slot.itemImage.transform.localScale = new Vector2(1.1f,1.1f);
            slot.bgSlot.sprite = slot.slotSprites[1];

            slot.itemImage.GetComponent<HoverItem>().Play();
        }
        if (slot.itemData)
        {
            UIManager.Instance.SetPanel(true, slot.itemData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slot.itemImage.transform.localScale = new Vector2(1f,1f);
        slot.bgSlot.sprite = slot.slotSprites[0];

        slot.itemImage.GetComponent<HoverItem>().Stop();

        if (slot.itemData)
        {
            UIManager.Instance.SetPanel(false);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (slot.itemData)
        {
            if (Input.GetKey(KeyCode.Q) && StorageManager.Instance.GetHoldingItem().IsEmpty())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    StorageManager.Instance.HoldItem(slot);
                    HoldingItem holdingItem = StorageManager.Instance.GetHoldingItem();
                    DroppingItem droppingItem = new DroppingItem(holdingItem.GetItemData(),holdingItem.GetAmount());
                    holdingItem.HoldItem();
                }
                if(Input.GetMouseButtonDown(1))
                {
                    StorageManager.Instance.HoldItem(slot, true); // Change to 1
                    HoldingItem holdingItem = StorageManager.Instance.GetHoldingItem();
                    DroppingItem droppingItem = new DroppingItem(holdingItem.GetItemData(),holdingItem.GetAmount());
                    holdingItem.HoldItem();
                }
            }
            else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if(Input.GetKey(KeyCode.LeftShift)) {
                        StorageManager.Instance.HoldItem(slot,true);
                        //StorageManager.Instance.HoldItemTEST(GetStorage(), slot.id, (int)Math.Ceiling((float)slot.amount / 2));
                    }
                    else
                    {
                        StorageManager.Instance.HoldItem(slot);
                    }                  
                }

                if(Input.GetMouseButtonDown(1) && StorageManager.Instance.GetHoldingItem().IsEmpty())
                {
                    if(slot.type == StorageTypes.Chest)
                    {
                        Debug.Log(slot);
                        StorageManager.Instance.HoldItem(slot);
                        StorageManager.Instance.GetStorage(0, StorageTypes.Bag).Add();

                    }
                    if(slot.type == StorageTypes.Shop)
                    {
                        if (slot.id < 3)
                        {
                            GameManager.Instance.GetNpc(GameManager.Instance.getPlayer().GetComponent<PlayerAction>().GetCurrentActionID()).GetComponent<Merchant>().BuyWare(slot.id);
                        }
                    }
                    else
                    {
                        if(slot.itemData.type == ItemType.Weapon)
                        {
                            StorageManager.Instance.HoldItem(slot);
                            StorageManager.Instance.GetStorage(0, StorageTypes.Armory).Add(slotID: 4);
                            return;
                        }
                        if(slot.itemData.type == ItemType.Armor)
                        {
                            int slotID = -1;
                            if(slot.itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Helmet){
                                slotID = 0;
                            }
                            if(slot.itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Body){
                                slotID = 1;
                            }
                            if(slot.itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Leggins){
                                slotID = 2;
                            }
                            if(slot.itemData.itemPrefab.GetComponent<Armor>().armorType == ArmorType.Boots){
                                slotID = 3;
                            }
                            if (slotID == -1)
                                return;

                            StorageManager.Instance.HoldItem(slot);
                            StorageManager.Instance.GetStorage(0, StorageTypes.Armory).Add(slotID: slotID);
                        }
                    }
                }
            }

        }
        if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
        {
            if(Input.GetMouseButtonDown(1))
            {
                StorageManager.Instance.FindStorage().Add(itemAmount: 1, slotID: StorageManager.Instance.FindSlot().id);
            }
        }
            
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
        {
            if(Input.GetMouseButtonUp(0))
            {
                Storage storage = StorageManager.Instance.FindStorage();
                storage.Add(slotID: StorageManager.Instance.FindSlot().id);
            }
        }   
    }
    Storage GetStorage()
    {
        if(slot.type == StorageTypes.Bag)
            return StorageManager.Instance.GetStorage(0,StorageTypes.Bag);
        if(slot.type == StorageTypes.Hotbar)
            return StorageManager.Instance.GetStorage(0,StorageTypes.Hotbar);
        if(slot.type == StorageTypes.Armory)
            return StorageManager.Instance.GetStorage(0,StorageTypes.Armory);
        if(slot.type == StorageTypes.Chest)
            return StorageManager.Instance.GetStorage(GameManager.Instance.getPlayer().GetComponent<PlayerAction>().GetCurrentActionID(),StorageTypes.Chest);
        return null;
    }
}
