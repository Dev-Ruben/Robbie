using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public int id;
    public new string name;
    public Sprite display;
    public ItemType type;
    public GameObject itemPrefab;

    [TextArea(10, 10)]
    public string description;
    public int maxDropAmount;
    public bool stackable;
    public int price;
}

public enum ItemType
{
    None,
    Weapon,
    Armor,
    Resource,
    Consumable
}
