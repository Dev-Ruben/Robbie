using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public int id;
    public Image itemImage;
    public Image bgSlot;
    public Sprite[] slotSprites;
    public ItemData itemData;
    public int amount;
    public TextMeshProUGUI amountDisplay;
    public GameObject bgAmount;
    public StorageTypes type;

    public void Fill(StoredItem storedItem)
    {
        itemData = storedItem.GetItemData();
        amount = storedItem.GetStackSize();
        amountDisplay.text = amount.ToString();
        //if (slot.amount > 1)
        bgAmount.SetActive(true);
        itemImage.enabled = true;
        itemImage.sprite = itemData.display;
    }

    public void Empty()
    {
        itemData =  null;
        amount = 0;
        amountDisplay.text = "";
        itemImage.sprite = null;
        itemImage.enabled = false;
        bgAmount.SetActive(false);
    }

    public int GetID()
    {
        return id;
    }


}
