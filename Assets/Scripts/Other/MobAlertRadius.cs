using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAlertRadius : MobRadius
{
    protected override void addRadius()
    {
        radius = 2f;
        radiusCollider = gameObject.AddComponent<CircleCollider2D>();
        radiusCollider.radius = radius;
        radiusCollider.isTrigger = true;
    }
}
