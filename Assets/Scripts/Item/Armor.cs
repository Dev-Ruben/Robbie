using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : Item
{
    public int defense;
    public int durability;
    public ArmorType armorType;

}

public enum ArmorType
{
    None,
    Helmet,
    Body,
    Leggins,
    Boots,
    Ring
}
