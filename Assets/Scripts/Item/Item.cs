using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        try { gameObject.GetComponent<SpriteRenderer>().sprite = itemData.display; }
        catch (MissingComponentException) { }


    }

    public GameObject getItemPrefab() {
        return itemData.itemPrefab;
    }
}

