using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, IActionable
{
    private int id = 0;
    private float minTax = 0.2f;
    private float maxTax = 0.5f;
    private float tax;

    //temp
    public ItemData buyitem;

    private Storage storage;

    private void Start(){
        GameManager.Instance.AddToNpcList(gameObject);
        StorageManager.Instance.CreateShop(id);
        storage = StorageManager.Instance.GetStorage(id,StorageTypes.Shop);

        tax = Random.Range(minTax,maxTax);

        //storage.Add(buyitem, 0);
    }
    public void UseAction()
    {
        UIManager.Instance.ToggleShop(id,GameManager.Instance.getPlayer().GetComponent<ID>().GetID());

    }
    public void UpdateMerchant()
    {

        


        StoredItem sellWare = storage.GetStoredItem(3);
        if (!sellWare.IsEmpty())
        {
            Balance playerBalance = StorageManager.Instance.GetBalance(GameManager.Instance.getPlayer().GetComponent<ID>().GetID());
            playerBalance.Add(SellWare(sellWare.GetItemData(), sellWare.GetStackSize()));
            UIManager.Instance.UpdateBalance(0);
            sellWare.EmptySlot();
            UIManager.Instance.UpdateShop(id);
        }
    }
    private int SellWare(ItemData itemData, int itemAmount)
    {   
        return TaxReduction(itemData.price) * itemAmount;  
        
    }
    public void BuyWare(int slotID)
    {
        StoredItem ware = storage.GetStoredItem(slotID);
        Balance playerBalance = StorageManager.Instance.GetBalance(GameManager.Instance.getPlayer().GetComponent<ID>().GetID());

        if (!ware.IsEmpty()){

            int totalCost = (ware.GetItemData().price * ware.GetStackSize());

            if (playerBalance.GetBalance() >= totalCost){

                bool hasRoom = StorageManager.Instance.GetStorage(0, StorageTypes.Bag).Add(ware.GetItemData(),ware.GetStackSize());
                if (hasRoom)
                {
                    playerBalance.Remove(totalCost);
                    ware.EmptySlot();
                }

            }
        }
    }

    public int TaxReduction(int price)
    {
        int newPrice = (int)(price * (1 - tax));
        return newPrice;
    }
}
