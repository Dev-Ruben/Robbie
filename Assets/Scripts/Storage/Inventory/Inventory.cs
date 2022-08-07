using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    void Update()
    {
        if (InputManager.Instance.getButtonDown("FlipPanel"))
        {
            UIManager.Instance.FlipPanel();
        }
        /*
        if (InputManager.Instance.getButtonDown("Drop"))
        {
            if (!StorageManager.Instance.GetHoldingItem().IsEmpty())
            {
                HoldingItem holdingItem = StorageManager.Instance.GetHoldingItem();
                DroppingItem droppingItem = new DroppingItem(holdingItem.GetItemData(),holdingItem.GetAmount());
                holdingItem.HoldItem();
            }
        }*/
    }
}
