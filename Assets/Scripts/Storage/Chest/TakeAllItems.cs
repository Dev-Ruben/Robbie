using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAllItems : MonoBehaviour
{

    public void GetCurrtenChestID()
    {
        TakeAll(GameManager.Instance.getPlayer().GetComponent<PlayerAction>().GetCurrentActionID());
    }

    private void TakeAll(int id)
    {
        foreach (StoredItem storedItem in StorageManager.Instance.GetStorage(id, StorageTypes.Chest).StoredItems())
        {
            if (storedItem.GetItemData())
            {
                bool canPlace = StorageManager.Instance.GetStorage(0,StorageTypes.Bag).Add(storedItem.GetItemData(),storedItem.GetStackSize());
                if(canPlace)
                    storedItem.EmptySlot();
            }
        }
        UIManager.Instance.UpdateBag(GameManager.Instance.getPlayer().GetComponent<ID>().GetID());
        UIManager.Instance.UpdateChest(id);
    }
}
