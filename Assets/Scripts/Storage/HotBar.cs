using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBar : MonoBehaviour
{
    UI ui;
    Slot[] slots;
    public GameObject selecter;
    int slotNr;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UI>();
        slots = ui.GetHotbarSlots();
        slotNr = 0;
        SelectSlot();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            slotNr--;
            if (slotNr < 0)
                slotNr = 6;

            SelectSlot();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            slotNr++;
            if (slotNr > 6)
                slotNr = 0;

            SelectSlot();
        }
    }

    void SelectSlot()
    {
        selecter.transform.position = slots[slotNr].transform.position;
    }
}
