using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class Equipment : MonoBehaviour
{
    public Weapon weapon;
    public Weapon secondaryWeapon;
    public Helmet helmetArmor;
    public Body bodyArmor;
    public Leggins legginsArmor;
    public Boots bootsArmor;
    [SerializeField] GameObject emptyHand;

    //#######temp code:
    public GameObject tempItem;
    //#######

    void Start()
    {
        weapon = null;
        secondaryWeapon = null;
        helmetArmor = null;
        bodyArmor = null;
        legginsArmor = null;
        bootsArmor = null;

        SpawnWeapon(emptyHand);
    }
    private void Update()
    {
        if (InputManager.Instance.getButtonDown("Weapon Swap"))
        {
            SwapWeapons();
        }

        setEquipmentPosition();

        /*#######temp code:
        try
        {
            if (weapon != tempItem.GetComponent<Weapon>())
            {
                
                try
                {
                    Destroy(transform.GetChild(2).GetChild(0).gameObject);
                }
                catch (UnityException) { }
                Debug.Log("instantiate Weapon");
                tempItem = Instantiate(tempItem, transform.position, Quaternion.identity);
                tempItem.transform.SetParent(gameObject.transform.GetChild(2));
                weapon = tempItem.GetComponent<Weapon>();
            }
        }
        catch (UnassignedReferenceException)
        {
            try
            {
                Destroy(transform.GetChild(2).GetChild(0).gameObject);
            }
            catch (UnityException) { }
            
            tempItem = Instantiate(emtyHand, transform.position, Quaternion.identity);
            tempItem.transform.SetParent(gameObject.transform.GetChild(2));
            weapon = tempItem.GetComponent<Weapon>();
        }
        */
    }

    public void Equip()
    {
        Storage armory = StorageManager.Instance.GetStorage(0,StorageTypes.Armory);
        foreach (StoredItem storedItem in armory.StoredItems())
        {
            if (storedItem.IsEmpty()){
                if (storedItem.GetSlotID() == (int)ArmorySlots.PrimaryWeaponSlot)
                {
                    tempItem = emptyHand;
                    continue;
                }
                if (storedItem.GetSlotID() == (int)ArmorySlots.SecondaryWeaponSlot)
                {
                    secondaryWeapon = null;
                    continue;
                }
                if (storedItem.GetSlotID() == (int)ArmorySlots.HelmetSlot)
                {
                    helmetArmor = null;
                    continue;
                }
                if (storedItem.GetSlotID() == (int)ArmorySlots.BodySlot)
                {
                    bodyArmor = null;
                    continue;
                }
                if (storedItem.GetSlotID() == (int)ArmorySlots.LegginsSlot)
                {
                    legginsArmor = null;
                    continue;
                }
                if (storedItem.GetSlotID() == (int)ArmorySlots.BootsSlot)
                {
                    bootsArmor = null;
                    continue;
                }
            }
            else
            {
                if(storedItem.GetItemData().type == ItemType.Weapon) 
                {
                    if (storedItem.GetSlotID() == (int)ArmorySlots.PrimaryWeaponSlot){

                        if (weapon == null || storedItem.GetID() != weapon.itemData.id){

                            weapon = storedItem.GetItemData().itemPrefab.GetComponent<Weapon>();
                            SpawnWeapon(storedItem.GetItemData().itemPrefab);//TEMP
                        }
                        continue;
                    }
                    if (storedItem.GetSlotID() == (int)ArmorySlots.SecondaryWeaponSlot){

                        if (secondaryWeapon == null || storedItem.GetID() != secondaryWeapon.itemData.id){

                            secondaryWeapon = storedItem.GetItemData().itemPrefab.GetComponent<Weapon>();
                        }
                        continue;
                    }
                }
                if(storedItem.GetItemData().type == ItemType.Armor) {

                    if (storedItem.GetSlotID() == (int)ArmorySlots.HelmetSlot){

                        if (helmetArmor == null || storedItem.GetID() != helmetArmor.itemData.id){

                            helmetArmor = storedItem.GetItemData().itemPrefab.GetComponent<Helmet>();
                        }
                        continue;
                    }
                    if (storedItem.GetSlotID() == (int)ArmorySlots.BodySlot){

                        if (bodyArmor == null || storedItem.GetID() != bodyArmor.itemData.id){

                            bodyArmor = storedItem.GetItemData().itemPrefab.GetComponent<Body>();
                        }
                        continue;

                    }
                    if (storedItem.GetSlotID() == (int)ArmorySlots.LegginsSlot){
                       
                        if (legginsArmor == null || storedItem.GetID() != legginsArmor.itemData.id){

                            legginsArmor = storedItem.GetItemData().itemPrefab.GetComponent<Leggins>();
                        }
                        continue;
                    }
                    if (storedItem.GetSlotID() == (int)ArmorySlots.BootsSlot){

                        if (bootsArmor == null || storedItem.GetID() != bootsArmor.itemData.id){

                            bootsArmor = storedItem.GetItemData().itemPrefab.GetComponent<Boots>();
                        }
                        continue;
                    }
                }
            }
        }
        GameManager.Instance.getPlayer().GetComponent<PlayerStats>().SetDefence(helmetArmor, bodyArmor, legginsArmor, bootsArmor);
    }
    // Use TAB to Swap the Primary and Secondary weapons
    public void SwapWeapons()
    {
        Storage armory = StorageManager.Instance.GetStorage(0,StorageTypes.Armory);
        StoredItem[] storedItems = armory.StoredItems();
        if (storedItems[(int)ArmorySlots.PrimaryWeaponSlot].GetID() != -1 || storedItems[(int)ArmorySlots.SecondaryWeaponSlot].GetID() != -1)
        {
            ItemData newPrimaryWeapon = storedItems[(int)ArmorySlots.SecondaryWeaponSlot].GetItemData();
            int newPrimaryWeaponAmount = storedItems[(int)ArmorySlots.SecondaryWeaponSlot].GetStackSize();
            ItemData newSecondaryWeapon = storedItems[(int)ArmorySlots.PrimaryWeaponSlot].GetItemData();
            int newSecondaryWeaponAmount = storedItems[(int)ArmorySlots.PrimaryWeaponSlot].GetStackSize();

            if(newSecondaryWeapon != null)
            {
                armory.StoredItems()[(int)ArmorySlots.SecondaryWeaponSlot].EmptySlot();
                armory.Add(newSecondaryWeapon, newSecondaryWeaponAmount, (int)ArmorySlots.SecondaryWeaponSlot);
            }
            else
                armory.StoredItems()[(int)ArmorySlots.SecondaryWeaponSlot].EmptySlot();

            if(newPrimaryWeapon != null)
            {
                armory.StoredItems()[(int)ArmorySlots.PrimaryWeaponSlot].EmptySlot();
                armory.Add(newPrimaryWeapon, newPrimaryWeaponAmount, (int)ArmorySlots.PrimaryWeaponSlot);
            }
            else
                armory.StoredItems()[(int)ArmorySlots.PrimaryWeaponSlot].EmptySlot();

            UIManager.Instance.UpdatePlayerSlots(0);
        }
    }

    void SpawnWeapon(GameObject weaponPrefab)
    {
        try
        {
            Destroy(transform.GetChild(2).GetChild(0).gameObject);
        }
        catch (UnityException) { }

        tempItem = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        tempItem.transform.SetParent(gameObject.transform.GetChild(2));

    }

    void setEquipmentPosition() {
        if (weapon != null) {
            weapon.transform.position = gameObject.transform.position;
        }

        if (helmetArmor != null)
        {
            helmetArmor.transform.position = gameObject.transform.position;
        }

        if (bodyArmor != null)
        {
            bodyArmor.transform.position = gameObject.transform.position;
        }
    }

}
