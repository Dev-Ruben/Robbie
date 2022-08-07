using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Punch : PlayerAttack
{
    public GameObject punchShockwave;
    PlayerAnimationController animationController;
    float charge_time;
    float set_time;

    private void Start()
    {

        mouse_info = transform.parent.GetComponentInParent<Mouse_info>();
        animationController = transform.parent.GetComponentInParent<PlayerAnimationController>();

        hitboxLiftime = 0.25f;
    }
    private void Update()
    {
        // timed attack, longer charge = stronger/bigger attack
        if (InputManager.Instance.getButtonDown("Attack"))
        {
            set_time = Time.time;
        }

        if (InputManager.Instance.getButtonUp("Attack"))
        {
            charge_time = Time.time - set_time;
            StartCoroutine(animationController.MeleeAttack());
            if (charge_time < 1.5f)
            {
                Spawn_hitbox(0.1f);
            }
            else
            {
                Spawn_hitbox(0.3f);
            }
                
            charge_time = 0;
        }

        TriggerDamageandKnockback(charge_time);

    }

    void Spawn_hitbox(float charge)
    {
        hitbox = Instantiate(punchShockwave, gameObject.transform.position, Quaternion.LookRotation(Vector3.forward, -mouse_info.mouse_direction));

        hitbox.transform.rotation = Quaternion.LookRotation(Vector3.forward, -mouse_info.mouse_direction);
        hitbox.transform.localScale = new Vector3(charge, charge, hitbox.transform.position.z);
        
        hitbox.name = "Melee_attack_hitbox";
        hitbox.transform.position = new Vector2(transform.position.x, transform.position.y) + mouse_info.mouse_direction.normalized * 0.3f;

        hitbox.GetComponent<PunchShockwave>().lifeTime = hitboxLiftime;

        hitbox.AddComponent<Hitbox_return_info>();
    }
}
