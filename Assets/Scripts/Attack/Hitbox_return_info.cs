using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_return_info : MonoBehaviour
{
    List<Collider2D> enemiesColliders;
    GameObject equipment;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        equipment = GameObject.Find("Equipment"); 
        if(collision.tag == "EnemyBody")
        {
            enemiesColliders = equipment.GetComponentInChildren<PlayerAttack>().enemiesColliders;
            enemiesColliders.Add(collision);
        }
    }
}
