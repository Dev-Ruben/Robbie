using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{ 
    public Text itemName;
    public Text itemDescription;
    public GameObject bottomPanel;
    public GameObject statPanel;
    public GameObject descriptionPanel;
    public Image itemImage;
    public GameObject itemStatBar;

    void Awake()
    {
        transform.position = Input.mousePosition;
    }

    void Update()
    {
        transform.position = Input.mousePosition;

        if(InputManager.Instance.getButtonDown("Flip Panel"))
        {

        }
    }
   
    public void SetInfo(ItemData itemData)
    {
        itemName.text = itemData.name;
        itemDescription.text = itemData.description;
        SetStats(itemData);
        //itemImage.sprite = itemData.display;
    }

    private void SetStats(ItemData itemData)
    {
        if (itemData.type == ItemType.Weapon)
        {
            Weapon weapon = itemData.itemPrefab.GetComponent<Weapon>();
            //StatBar statbar = itemStatBar.GetComponent<StatBar>();
            statPanel.GetComponentInChildren<Text>().text = "Damage: " + weapon.damage + "\n" + "Knockback: " + weapon.knockback;
        }
        if(itemData.type == ItemType.Armor)
        {
            Armor armor = itemData.itemPrefab.GetComponent<Armor>();
            statPanel.GetComponentInChildren<Text>().text = "Defense: " + armor.defense + "\n" + "Durability: " + armor.durability;
        }
        else
        {
            statPanel.GetComponentInChildren<Text>().text = "Other Stats";
        }
    }
}
