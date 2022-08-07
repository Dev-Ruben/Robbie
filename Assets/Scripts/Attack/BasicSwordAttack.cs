using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwordAttack : PlayerAttack
{
    public GameObject slash;
    PlayerAnimationController animationController;
    float charge_time;
    float set_time;
    float charge;

    private void Start()
    {
        mouse_info = transform.parent.GetComponentInParent<Mouse_info>();
        animationController = transform.parent.GetComponentInParent<PlayerAnimationController>();

        hitboxLiftime = 0.25f;
    }
    private void Update()
    {
        // time attack, longer charge = stronger/bigger attack
        if (InputManager.Instance.getButtonDown("Attack"))
        {
            set_time = Time.time;
        }

        if (InputManager.Instance.getButtonUp("Attack"))
        {
            charge_time = Time.time - set_time;
            charge = Mathf.Lerp(0.05f, 0.5f, charge_time * 0.5f);
            StartCoroutine(animationController.MeleeAttack());
            Spawn_hitbox(charge);
            charge_time = 0;
        }

        foreach (Collider2D enemy in enemiesColliders)
        {
            try
            {
                DamageEnemy(enemy, GetComponent<Weapon>().damage * charge * 100);
                KnockbackEnemy(enemy, GetComponent<Weapon>().knockback * charge * 10);
            }

            catch (System.IndexOutOfRangeException) { }

        }
        enemiesColliders.Clear();

    }
        void Spawn_hitbox(float scale)
        {
            hitbox = Instantiate(slash, gameObject.transform.position, Quaternion.LookRotation(Vector3.forward, -mouse_info.mouse_direction));

            hitbox.transform.rotation = Quaternion.LookRotation(Vector3.forward, -mouse_info.mouse_direction);
            hitbox.transform.localScale = new Vector3(scale, scale, hitbox.transform.position.z);
            hitbox.name = "Melee_attack_hitbox";
            hitbox.transform.position = new Vector2(transform.position.x, transform.position.y) + mouse_info.mouse_direction.normalized * 0.3f;
            hitbox.transform.parent = gameObject.transform;

            hitbox.GetComponent<Slash>().lifeTime = hitboxLiftime;

            hitbox.AddComponent<Hitbox_return_info>();
        }
    }
