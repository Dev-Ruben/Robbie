using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTypes : MonoBehaviour
{
    GameObject _item;
    public void GetItemInfo(GameObject item)
    {
        _item = item;
        Types(item.tag);
    }

    void Types(string type)
    {
        if (type == "Sword")
        {
            _item.GetComponent<Sword>();
        }
        if (type == "Bow")
        {
            _item.GetComponent<Bow>();
        }
    }
}
