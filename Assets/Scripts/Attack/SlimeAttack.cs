using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : Attack
{
    CapsuleCollider2D mobHitBox;
    MobAttackRadius mobAttackRadius;
    Vector2 attackDirection;
    public float slimeDashSpeed;
    Rigidbody2D slimeBody;
    GameObject player;
    public float knockback_power;

    // Start is called before the first frame update
    void Start()
    {
        mobHitBox = gameObject.transform.GetChild(0).gameObject.GetComponent<CapsuleCollider2D>();
        mobAttackRadius = gameObject.transform.GetChild(2).gameObject.GetComponent<MobAttackRadius>();
        slimeBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        startAttack();
    }
    private void startAttack()
    {
        if (mobAttackRadius.isInRange() == true && attacking == false)
        {
            StartCoroutine(melee_attack_timer());
            attacking = true;
        }
    }
    IEnumerator melee_attack_timer()
    {
        attacking = true;
        
        attackDirection = mobAttackRadius.GetPlayer().GetComponent<Transform>().position - transform.position;
        gameObject.GetComponent<MobMovement>().enabled = false;
        

        attack();        

        yield return new WaitForSeconds(1.5f);

        if (gameObject.GetComponent<CharacterStats>().hasBeenKilled() == false)
        {
            gameObject.GetComponent<MobMovement>().enabled = true;
        }

        yield return new WaitForSeconds(0.1f);

        attacking = false;
    }

    private void attack()
    {
        slimeBody.AddForce(attackDirection.normalized * slimeDashSpeed, ForceMode2D.Impulse);
    }

    private void knockback()
    {
        player.GetComponent<Rigidbody2D>().AddForce(attackDirection.normalized * knockback_power, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider.IsTouching(mobHitBox))
        {
            player = collision.gameObject;
            playerStats = player.GetComponentInParent<PlayerStats>();

            if (playerStats)
            {
                mobStats = gameObject.GetComponent<MobStats>();
                DoDamage(playerStats, mobStats.getDamage().GetValue());
                knockback();
            }
        }
    }
    private void Ontriggerenter(Collider2D collision)
    {

        
    }
}
