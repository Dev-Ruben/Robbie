using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : Movement
{
    Vector2 ray_hit_position_N;
    Vector2 ray_hit_position_S;
    Vector2 ray_hit_position_E;
    Vector2 ray_hit_position_W;

    public float distants_to_wall = 0.001f;

    RaycastHit2D raycast_hit_N;
    RaycastHit2D raycast_hit_S;
    RaycastHit2D raycast_hit_E;
    RaycastHit2D raycast_hit_W;

    Vector2 body_position;

    float collider_height;
    float collider_width;

    private void Start()
    {
        collider_height = GetComponentInChildren<BoxCollider2D>().size.y;
        collider_width = GetComponentInChildren<BoxCollider2D>().size.x;
    }

    void Update()
    {
        body_position = new Vector2(transform.position.x, transform.position.y) - GetComponentInChildren<BoxCollider2D>().offset;
        Wallcheck();
    }

    private void Wallcheck()
    {

        LayerMask mask = LayerMask.GetMask("Physical");

        if(gameObject.tag != "Player" )
        {
            mask += LayerMask.GetMask("Player");
        }
        if (gameObject.tag != "Mob")
        {
            mask += LayerMask.GetMask("Enemy");
        }



        raycast_hit_N = Physics2D.Raycast(body_position, new Vector2(0, 1), collider_height/2 , mask);
        raycast_hit_S = Physics2D.Raycast(body_position, new Vector2(0,-1), collider_height/2, mask);
        raycast_hit_E = Physics2D.Raycast(body_position, new Vector2(1, 0), collider_width/2, mask);
        raycast_hit_W = Physics2D.Raycast(body_position, new Vector2(-1,0), collider_width/2, mask);

        if (raycast_hit_N.collider != null)
        {
            if (ray_hit_position_N == Vector2.zero)
            {
                ray_hit_position_N = transform.position;
            }

            if (transform.position.y >= ray_hit_position_N.y)
            {
                transform.position = new Vector2(transform.position.x, ray_hit_position_N.y);
            }
        }
        else
        {
            ray_hit_position_N = Vector2.zero;
        }

        if (raycast_hit_S.collider != null)
        {
            if (ray_hit_position_S == Vector2.zero)
            {
                ray_hit_position_S = transform.position;
            }

            if (transform.position.y <= ray_hit_position_S.y)
            {
                transform.position = new Vector2(transform.position.x, ray_hit_position_S.y);
            }

        }
        else
        {
            ray_hit_position_S = Vector2.zero;
        }

        if (raycast_hit_E.collider != null)
        {
            if (ray_hit_position_E == Vector2.zero)
            {
                ray_hit_position_E = transform.position;
            }

            if (transform.position.x >= ray_hit_position_E.x)
            {
                transform.position = new Vector2(ray_hit_position_E.x, transform.position.y);
            }

        }
        else
        {
            ray_hit_position_E = Vector2.zero;
        }

        if (raycast_hit_W.collider != null)
        {
            if (ray_hit_position_W == Vector2.zero)
            {
                ray_hit_position_W = transform.position;
            }

            if (transform.position.x <= ray_hit_position_W.x)
            {
                transform.position = new Vector2(ray_hit_position_W.x, transform.position.y);
            }

        }
        else
        {
            ray_hit_position_W = Vector2.zero;
        }
    }
}
