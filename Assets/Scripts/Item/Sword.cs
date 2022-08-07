using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public int sharpness;
    public float hitbox_size;
    public string hitbox_shape;

    public Sword GetItemType()
    {
        return this;
    }
}
