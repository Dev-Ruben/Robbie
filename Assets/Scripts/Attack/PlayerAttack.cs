using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : Attack
{
    protected Mouse_info mouse_info;
    protected GameObject hitbox;
    protected float hitboxLiftime;
    protected GameObject weapon;
    protected float attackTimer = 1f;

    public List<Collider2D> enemiesColliders;

    Rigidbody2D enemyrb;

    Vector2 knockback_direction;

    private void Start()
    {
        mouse_info = GetComponent<Mouse_info>();
    }

    protected void DamageEnemy(Collider2D enemy, float damage)
    {
            mobStats = enemy.gameObject.GetComponentInParent<MobStats>();
            if (mobStats)
            {
                //playerStats = gameObject.GetComponent<CharacterStats>();
                DoDamage(mobStats, damage);
            }
    }

    protected void KnockbackEnemy(Collider2D enemy, float knockback)
    {
        knockback_direction = mouse_info.mouse_direction;
        enemyrb = enemy.GetComponentInParent<Rigidbody2D>();
        enemyrb.AddForce(knockback_direction.normalized * knockback, ForceMode2D.Impulse);
    }

    protected void TriggerDamageandKnockback(float charge_time)
    {
        foreach (Collider2D enemy in enemiesColliders)
        {
            try
            {
                if (charge_time < 1.5f)
                {
                    DamageEnemy(enemy, GetComponent<Weapon>().damage);
                    KnockbackEnemy(enemy, GetComponent<Weapon>().knockback);
                }
                else
                {
                    DamageEnemy(enemy, GetComponent<Weapon>().damage * 3);
                    KnockbackEnemy(enemy, GetComponent<Weapon>().knockback * 3);
                }
            }

            catch (System.IndexOutOfRangeException) { }
        }
        enemiesColliders.Clear();
    }


}
