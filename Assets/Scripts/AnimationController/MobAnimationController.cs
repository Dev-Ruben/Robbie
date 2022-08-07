using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobAnimationController : AnimationController
{
    protected Animator mobAnimator;

    
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<MobStats>();
        movement = GetComponent<MobMovement>();
        attack = GetComponent<Attack>();
        mobAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.hasBeenKilled()) {
            dieAnimation();
        }
    }

    protected override void moveCharacter()
    {
        mobAnimator.SetBool("IsMoving", movement.isMoving);
    }

    protected override void attackCharacter() {
        mobAnimator.SetBool("IsAttacking", attack.IsAttacking());
    }

    protected override void turnCharacter()
    {
        mobAnimator.SetFloat("LastMove", movement.lastMove);
    }

    protected virtual void dieAnimation() {
    }
}
