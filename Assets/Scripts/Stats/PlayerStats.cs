using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    Equipment equipment;

    //##TEMP
    [SerializeField] float pDamage;
    [SerializeField] float pDefense;
    [SerializeField] float pSpeed;

    protected override void Awake()
    {
        base.Awake();

        baseDamage.SetValue(pDamage);
        baseDefense.SetValue(pDefense);
        baseSpeed.SetValue(pSpeed);
        SetDefence();
    }

    private void Update()
    {
        
    }
    public override void Die()
    {
        GameManager.Instance.currentGameState = GameState.PlayerDied;
    }

    public void SetDefence(Helmet helmet = null, Body body = null, Leggins leggins = null, Boots boots = null)
    {
        totalDefense.ResetValue();
        totalDefense.AddValue(baseDefense.GetValue());

        if(helmet != null){
            totalDefense.AddValue(helmet.defense);
        }
        if(body != null){
            totalDefense.AddValue(body.defense);
        }
        if(leggins != null){
            totalDefense.AddValue(leggins.defense);
        }
        if(boots != null){
            totalDefense.AddValue(boots.defense);
        }
    }

    /* Has to be maybe moved to inventory, maybe not
    private void SetDamage()
    {
        if (gameObject.GetComponent<Equipment>().weapon != null)
        {
            
            damage.AddValue(gameObject.GetComponent<Equipment>().weapon.damage);
        }

    }
    */
}
