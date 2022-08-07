using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttackRadius : MobRadius
{
    protected override void addRadius()
    {
        radius = 0.5f;
        radiusCollider = gameObject.AddComponent<CircleCollider2D>();
        radiusCollider.radius = radius;
        radiusCollider.isTrigger = true;
    }
}
