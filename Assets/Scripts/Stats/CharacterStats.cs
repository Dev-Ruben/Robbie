using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    protected int maxHealth = 100;
    protected float currentHealth { get; set; }
    protected bool hasDied = false;
    protected bool isAgitated = false;

    protected Stat baseDamage;
    protected Stat baseDefense;
    protected Stat baseSpeed;

    protected Stat totalDamage;
    protected Stat totalDefense;
    protected Stat totalSpeed;

    private float agitationDuration = 5f;

    public delegate void OnTakeDamage();
    public static event OnTakeDamage onTakeDamage;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        currentHealth = maxHealth;

        baseDamage = new Stat();
        baseDefense = new Stat();
        baseSpeed = new Stat();

        totalDamage = new Stat();
        totalDefense = new Stat();
        totalSpeed = new Stat();
    }

    public void TakeDamage(float damage) {
        float negatedDamage = damage - totalDefense.GetValue();
        if (negatedDamage > 0){
            currentHealth -= negatedDamage;
        }
        else{
            //maby Miss?
            currentHealth -= 1;
            Debug.Log("So bad, ill give you 1 dmg cuz im kind");
        }
        onTakeDamage();
        StartCoroutine("AgitateCharacter");


        if (currentHealth <= 0)
        {
            hasDied = true;
            Die();
        }
    }

    private IEnumerator AgitateCharacter() {
        isAgitated = true;
        yield return new WaitForSeconds(agitationDuration);
        if (isAgitated == true)
        {
            isAgitated = false;
        }

        StopCoroutine("AgitateCharacter");
    }

    public float getCurrentHealth() {
        return currentHealth;
    }
    public float getMaxHealth() {
        return maxHealth;
    }

    public Stat getDamage() {
        return baseDamage;
    }

    public Stat getDefense()
    {
        return baseDefense;
    }

    public Stat getSpeed()
    {
        return baseSpeed;
    }

    public bool hasBeenKilled() {
        return hasDied;
    } 

    public bool HasBeenAgitated() {
        return isAgitated;
    }

    public virtual void Die()
    {
    }

}
