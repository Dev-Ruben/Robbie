using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
        PlayerStats playerStats;
        public Image equipedWeaponImage;
        public GameObject inventory;
        public GameObject bag;
        public GameObject pouch;
        public GameObject armory;
        public GameObject statsBar;
        public GameObject hotbar;
        public GameObject weaponSlot;
        public GameObject miniMap;
        public GameObject chest;
        public GameObject shop;
        private  Slider healthBar;
        private  Slider staminaBar;
        Slot[] bagSlots;
        Slot[] armorySlots;
        Slot[] hotbarSlots;
        Slot[] chestSlots;
        ShopSlot[] shopSlots;
        List<Slot> allSlots;
        [SerializeField] GameObject customCursor;
        [SerializeField] GameObject infoPanel;
        GameObject hoveringSlot;



    void Awake()
        {
            playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
            inventory = GameObject.FindWithTag("Inventory");
            statsBar = GameObject.FindWithTag("StatsBar");
            hotbar = GameObject.FindWithTag("Hotbar");
            weaponSlot = GameObject.FindWithTag("WeaponSlot");
            miniMap = GameObject.FindWithTag("Minimap");

            bag = inventory.transform.GetChild(0).gameObject;
            armory = inventory.transform.GetChild(1).gameObject;

            bagSlots = new Slot[24];
            armorySlots = new Slot[6];
            hotbarSlots = new Slot[7];
            chestSlots = new Slot[6];
            shopSlots = new ShopSlot[4];
            allSlots = new List<Slot>();

            for (int slotID = 0; slotID < bagSlots.Length; slotID++)
            {
                Slot slot = bag.transform.GetChild(slotID).GetComponent<Slot>();
                slot.itemImage.enabled = false;
                slot.bgAmount.SetActive(false);
                slot.amountDisplay.text = "";
                slot.id = slotID;
                slot.type = StorageTypes.Bag;
                bagSlots[slotID] = slot;

                allSlots.Add(slot);
            }
            for (int slotID = 0; slotID < armorySlots.Length; slotID++)
            {
                Slot slot = new Slot();
                if (slotID < 4)
                        slot = armory.transform.GetChild(slotID).GetComponent<Slot>();
                else if (slotID >= 4)
                        slot = weaponSlot.transform.GetChild(slotID - 4).GetComponent<Slot>();

                slot.itemImage.enabled = false;
                slot.bgAmount.SetActive(false);
                slot.amountDisplay.text = "";
                slot.id = slotID;
                slot.type = StorageTypes.Armory;
                armorySlots[slotID] = slot;

                allSlots.Add(slot);
            }
            for (int slotID = 0; slotID < hotbarSlots.Length; slotID++)
            {
                Slot slot = hotbar.transform.GetChild(slotID).GetComponent<Slot>();
                slot.itemImage.enabled = false;
                slot.bgAmount.SetActive(false);
                slot.amountDisplay.text = "";
                slot.id = slotID;
                slot.type = StorageTypes.Hotbar;
                hotbarSlots[slotID] = slot;

                allSlots.Add(slot);
            } 
            for (int slotID = 0; slotID < chestSlots.Length; slotID++)
            {
                Slot slot = chest.transform.GetChild(slotID).GetComponent<Slot>();
                slot.itemImage.enabled = false;
                slot.bgAmount.SetActive(false);
                slot.amountDisplay.text = "";
                slot.id = slotID;
                slot.type = StorageTypes.Chest;
                chestSlots[slotID] = slot;

                allSlots.Add(slot);
            }
            for (int slotID = 0; slotID < shopSlots.Length; slotID++)
            {
                ShopSlot slot = shop.transform.GetChild(slotID).GetComponent<ShopSlot>();
                slot.itemImage.enabled = false;
                slot.bgAmount.SetActive(false);
                slot.amountDisplay.text = "";
                slot.id = slotID;
                slot.type = StorageTypes.Shop;
                shopSlots[slotID] = slot;

                allSlots.Add(slot);
            }

            healthBar = statsBar.transform.GetChild(0).GetComponent<Slider>();
            staminaBar = statsBar.transform.GetChild(1).GetComponent<Slider>();

            infoPanel.SetActive(false);
            bag.SetActive(false);
            armory.SetActive(false);
            chest.SetActive(false);
            shop.SetActive(false);
            inventory.SetActive(false);
            customCursor.GetComponent<Image>().enabled = false;
        }
        
        

        public Slot[] GetBagSlots()
        {
                return bagSlots;
        }
        public Slot[] GetArmorySlots()
        {
                return armorySlots;
        }
        public Slot[] GetHotbarSlots()
        {
                return hotbarSlots;
        }
        public Slot[] GetChestSlots()
        {
                return chestSlots;
        }
        public ShopSlot[] GetShopSlots()
        {
                return shopSlots;
        }
        public List<Slot> GetAllSlots()
        {
                return allSlots;
        }

        public GameObject GetInfoPanel()
        {
                return infoPanel;
        }

        public GameObject GetCustomCursor()
        {
                return customCursor;
        }
        
        public GameObject GetStatsBar()
        {
                return statsBar;
        }
        public GameObject GetHotbar()
        {
                return hotbar;
        }
        public GameObject GetChest()
        {
                return chest;
        }
        public GameObject GetMiniMap()
        {
                return miniMap;
        }
        public GameObject GetWeaponSlot()
        {
                return weaponSlot;
        }
        public GameObject GetPouch()
        {
                return pouch;
        }
         //Move to UI interactions
       
        public void SetHealthBar(float currentHealth)
        {
                healthBar.value = currentHealth;
        }
        public void SetMaxHealthBar(float maxHealth)
        {
                healthBar.maxValue = maxHealth;
        }

        public void SetStaminaBar(float currentStamina)
        {
                staminaBar.value = currentStamina;
        }
        public void SetMaxStaminaBar(float maxStamina)
        {
                staminaBar.maxValue = maxStamina;
        }

        
        

        
}
