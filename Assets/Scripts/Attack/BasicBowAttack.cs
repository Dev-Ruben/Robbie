using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBowAttack : PlayerAttack
{
    public GameObject arrow;
    float charge_time;
    float set_time;
    float charge;

    private void Start()
    {
        mouse_info = transform.parent.GetComponentInParent<Mouse_info>();
        hitboxLiftime = 1f;
    }

    private void Update()
    {
        if (InputManager.Instance.getButtonDown("Attack"))
        {
            set_time = Time.time;
        }

        if (InputManager.Instance.getButtonUp("Attack"))
        {
            charge_time = Time.time - set_time;
            charge = Mathf.Lerp(0.05f, 0.5f, charge_time * 0.5f);
            Spawn_ranged_Hitbox(charge);
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
    private void Spawn_ranged_Hitbox(float scale)
    {
        hitbox = Instantiate(arrow, gameObject.transform.position, Quaternion.LookRotation(Vector3.forward, -mouse_info.mouse_direction));
        hitbox.name = "projectile";
        //hitbox.transform.localScale = new Vector3(scale*0.1f, scale*0.1f, hitbox.transform.position.z);
        hitbox.AddComponent<Hitbox_return_info>();

        hitbox.GetComponent<Arrow>().lifeTime = 0.4f + scale;
        hitbox.GetComponent<Arrow>().flight_speed = 0.15f + scale*0.3f;
    }
}
