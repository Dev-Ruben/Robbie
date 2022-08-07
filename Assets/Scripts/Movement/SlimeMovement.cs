using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MobMovement
{
    Rigidbody2D slime_rb;
    Vector2 player_direction;
    public AudioSource audioWalk;
    public float movement_speed;
    bool inrange;

    private void Start()
    {
        slime_rb = gameObject.GetComponent<Rigidbody2D>();
        
    }

    protected override void moveCharacter()
    {
        if (gameObject.transform.GetChild(1).GetComponent<MobAlertRadius>().isInRange() == true)
        {
            if (!isMoving){
                isMoving = true;
                audioWalk.Play();

            }
            player_direction = gameObject.transform.GetChild(1).GetComponent<MobAlertRadius>().GetPlayer().GetComponent<Transform>().position - transform.position;
        }
        else
        {
            isMoving = false;
            audioWalk.Stop();
        }

    }


    private void FixedUpdate()
    {
        if(isMoving == true)
        {
            slime_rb.AddForce(player_direction.normalized * movement_speed, ForceMode2D.Force);
        }
    }
}
