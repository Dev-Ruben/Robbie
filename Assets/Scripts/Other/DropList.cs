using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class DropList : MonoBehaviour
{
    [SerializeField] ItemDropWeight[] itemList;
    List<ItemData> dropList = new List<ItemData>();

    int weight;

    void Start()
    {
        foreach(ItemDropWeight item in itemList)
        {   

            item.SetMinWeight(weight + 1);
            item.SetMaxWeight(weight + item.itemWeight);
            weight = item.GetMaxWeight();
        }
    }
    
    [System.Serializable]
    public class ItemDropWeight
    {
        public ItemData itemData;
        public int itemWeight;
        private int minWeight;
        private int maxWeight;

        public int GetMinWeight() {return minWeight;}
        public void SetMinWeight(int weight) {minWeight = weight;}
        public int GetMaxWeight() {return maxWeight;}
        public void SetMaxWeight(int weight) {maxWeight = weight;}
    }
    
    public void DropItem()
    {
        Drop(ChooseDrop());
    }

    private ItemData ChooseDrop()
    {
        if (itemList != null)
        {
            weight = itemList[itemList.Count() - 1].GetMaxWeight();
            int dropWeight = Random.Range(1,weight + 1);

            foreach(ItemDropWeight item in itemList)
            {
                if(dropWeight >= item.GetMinWeight() && dropWeight <= item.GetMaxWeight())
                {
                    return item.itemData;
                }
            }
        }
        return null;
    }

    private void Drop(ItemData dropItem)
    {
        int dropAmount = Random.Range(1,dropItem.maxDropAmount);
        //for(int dropCount = 0;  dropCount <= Random.Range(1, 2); dropCount++)
        //{
        GameObject droppedItem =  new GameObject();
        droppedItem.name = dropItem.name;
        droppedItem.AddComponent<SpriteRenderer>().sprite = dropItem.display;
        droppedItem.transform.localScale = new Vector2(0.27f,0.27f);
        droppedItem.AddComponent<CircleCollider2D>().isTrigger = true;
        droppedItem.AddComponent<Rigidbody2D>().gravityScale = 0;
        droppedItem.AddComponent<PickUp>().SetItem(dropItem,dropAmount);

        droppedItem.transform.position = gameObject.transform.position;
        //}
        
    }
}
