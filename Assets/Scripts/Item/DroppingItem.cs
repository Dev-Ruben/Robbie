using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingItem : MonoBehaviour
{

    float dropSpeed = 0.4f;
    float dropTime = 1f;
    GameObject droppedItem;

    ItemData item;
    int amount;
    
    public DroppingItem(ItemData dropItem, int dropAmount)
    {
        item = dropItem;
        amount = dropAmount;

        droppedItem = new GameObject();

        droppedItem.name = item.name;
        droppedItem.AddComponent<SpriteRenderer>().sprite = dropItem.display;
        droppedItem.transform.localScale = new Vector2(0.27f,0.27f);
        droppedItem.AddComponent<CircleCollider2D>().isTrigger = true;
        droppedItem.AddComponent<Rigidbody2D>().gravityScale = 0;

        droppedItem.transform.position = GameManager.Instance.getPlayer().transform.position;

        droppedItem.AddComponent<PickUp>().SetItem(item,amount);
    }
}
