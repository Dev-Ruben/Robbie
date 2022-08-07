using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopSlot : Slot
{

    int sellPrice;
    [SerializeField]
    public TextMeshProUGUI priceText;
    [SerializeField]
    public SlotType slotType;
   
}

public enum SlotType
{
    None,
    Buy,
    Sell
}
