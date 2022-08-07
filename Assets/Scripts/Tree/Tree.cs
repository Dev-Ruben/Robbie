using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IBreakable
{
    int health = 5;
    public void Break(int breakdamage)
    {
        if (health <= 1)
        {
            GetComponent<DropList>().DropItem();
            Destroy(gameObject);
        }
        else
        {
            health -= breakdamage;
        }
    }
}
