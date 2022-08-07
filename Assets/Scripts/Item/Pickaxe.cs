using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Weapon
{

    public void mineOre() { }
    public void chopTree() { }

    public Pickaxe GetItemType()
    {
        return this;
    }
}
