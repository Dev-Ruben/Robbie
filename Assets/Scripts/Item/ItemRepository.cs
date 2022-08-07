using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemRepository : SingletonMonobehaviour<ItemRepository>
{
    [SerializeReference]
    List<Item> items;

    protected override void Awake()
    {
        base.Awake();
        GameObject[] allItems = Resources.LoadAll("Prefabs/Items").Cast<GameObject>().ToArray();
        
        foreach(GameObject item in allItems)
        {
            Item newItem = (Item)item.GetComponent(typeof(Item));
            newItem.GetComponent<Item>().itemData.id = items.Count;
            //newItem.GetComponent<Item>().SetID();
            items.Add(newItem);
        }
    }


    public Item GetItem(int itemID)
    {
        return items[itemID];
    }
}
