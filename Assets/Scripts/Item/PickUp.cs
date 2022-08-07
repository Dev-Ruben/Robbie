using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private ItemData itemData;
    private int itemAmount;

    float dropSpeed;
    float pickupCooldown;
    public  bool enablePickup;
    void Start()
    {
        dropSpeed = 0.4f;
        pickupCooldown = 0.5f;
        enablePickup = false;

        StartCoroutine(EnablePickup());
    }

    public void SetItem(ItemData data, int amount)
    {
        itemData = data;
        itemAmount = amount;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(enablePickup)
        {
            if (coll.transform.tag == "PlayerBody")
            {
                bool pickedUp = StorageManager.Instance.GetStorage(coll.transform.parent.GetComponent<ID>().GetID(), StorageTypes.Bag).Add(itemData, itemAmount); 
                if (pickedUp)
                {
                    UIManager.Instance.UpdatePlayerSlots(coll.transform.parent.GetComponent<ID>().GetID());
                    Destroy(gameObject);
                }
            }
        }
    }

    private IEnumerator EnablePickup()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 dropDirection = GameManager.Instance.getPlayer().GetComponent<Mouse_info>().mouse_direction;
        rb.AddForce(dropDirection.normalized * dropSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(pickupCooldown);
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0; 
        enablePickup = true;
    }
}

   
