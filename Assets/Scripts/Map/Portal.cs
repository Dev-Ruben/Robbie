using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] PortalPillar[] pillars;
    [SerializeField] private bool opened = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void choosePillarItems()
    {
        Item item = new Item();

        foreach (PortalPillar pillar in pillars) {
            pillar.setItemNeeded(item);
        }
    }


    public void checkPillarFilled() {
        foreach (PortalPillar pillar in pillars) {
            if (pillar.isPillarFilled()) {
                opened = true;
            }
            else
            {
                opened = false;
            }
        }


    }

    public bool PortalIsOpen() {
        return opened;
    }
}
