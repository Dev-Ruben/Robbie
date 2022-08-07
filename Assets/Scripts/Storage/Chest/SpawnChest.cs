using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChest : MonoBehaviour
{
    Mouse_info mouseInfo;
    public GameObject chest;

    void Start()
    {
        mouseInfo = GameObject.Find("Robbie").GetComponent<Mouse_info>();
    }

    void Update()
    {
        if (InputManager.Instance.getButtonDown("Chest"))
        {
            int chestID = StorageManager.Instance.CreateChest();
            GameObject newChest = Instantiate(chest, mouseInfo.mouse_position,Quaternion.identity);
            newChest.GetComponent<Chest>().SetID(chestID);
        }
    }
}
