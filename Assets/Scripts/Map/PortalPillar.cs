using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPillar : MonoBehaviour, IActionable
{
    [SerializeField] Item itemNeeded;
    [SerializeField] bool filled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void forfillItem(int itemID) {
        if (!isPillarFilled()) {
            if (itemID == itemNeeded.itemData.id)
            {
                filled = true;
            }
            
        }
        
    }

    public void UseAction()
    {
        Storage playerStorage = StorageManager.Instance.GetStorage(
            GameManager.Instance.getPlayer().GetComponent<ID>().GetID(),
            StorageTypes.Bag);

        foreach (StoredItem item in playerStorage.StoredItems()) {

            if (item.GetItemData() != null) {
                forfillItem(item.GetItemData().id);
                Debug.Log(item.GetItemData().id);
            }
                         
        }
    }

    public void setItemNeeded(Item item)
    {
        itemNeeded = item;
    }

    public Item getItemNeeded() {
        return itemNeeded;
    }

    public bool isPillarFilled() {
        return filled;
    }
  
    
}
