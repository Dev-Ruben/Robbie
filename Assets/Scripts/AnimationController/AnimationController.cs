using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    protected Attack attack;
    protected CharacterStats stats;
    protected Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        stats = GetComponent<CharacterStats>();

        movement = GetComponent<Movement>();      
    }

    // Update is called once per frame
    void Update()
    {       
        turnCharacter();
        moveCharacter();
        attackCharacter();
        agitateCharacter();
    }

    protected virtual void moveCharacter() { }
    protected virtual void attackCharacter() { }
    protected virtual void agitateCharacter() { }
    protected virtual void turnCharacter() { }

}
