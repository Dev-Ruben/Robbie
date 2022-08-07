using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationController : MobAnimationController
{

    void Update()
    {
        moveCharacter();
        turnCharacter();
        attackCharacter();
        if (stats.hasBeenKilled())
        {
            dieAnimation();
        }
    }
    protected override void dieAnimation()
    {
        mobAnimator.SetBool("HasDied", stats.hasBeenKilled());
    }
}
