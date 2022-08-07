using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStats : CharacterStats
{

    protected override void Awake()
    {
        base.Awake();
        baseDamage.SetValue(15f);
        baseDefense.SetValue(10f);
        baseSpeed.SetValue(0.3f);

        totalDefense.AddValue(baseDefense.GetValue());
    }

    public override void Die()
    {
        gameObject.GetComponent<MobMovement>().enabled = false;
        gameObject.GetComponent<Attack>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        gameObject.GetComponent<DropList>().DropItem();
        Destroy(gameObject, 1.15f);
        
    }

    private void Update()
    {

    }
}
