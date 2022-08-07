using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobRadius : MonoBehaviour
{
    protected float radius;
    protected GameObject player;

    protected bool inRange;

    protected CircleCollider2D radiusCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        addRadius();
    }

    private void Update()
    {
        triggerRadius();
    }

    protected virtual void addRadius()
    {
        radiusCollider = gameObject.AddComponent<CircleCollider2D>();
        radiusCollider.radius = radius;
        radiusCollider.isTrigger = true;
    }

    private void triggerRadius() {
        if (radiusCollider.IsTouching(player.transform.GetChild(0).GetComponent<CapsuleCollider2D>()))
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    public bool isInRange() {
        return inRange;
    }


    public GameObject GetPlayer()
    {
        return player;
    }
}
