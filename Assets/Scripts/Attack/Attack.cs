using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    protected CharacterStats playerStats;
    protected CharacterStats mobStats;

    protected bool attacking;
    protected bool hasAttacked = false;

    public void DoDamage(CharacterStats characterStats, float damage)
    {
        characterStats.TakeDamage(damage);
    }

    public bool IsAttacking() {
        return attacking;
    }
}
