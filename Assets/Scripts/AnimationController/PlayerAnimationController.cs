using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    Equipment equipment;
    PlayerMovement playerMovement;

    Mouse_info mouseInfo;

    Animator playerAnimator;
    Animator weaponAnimator;
    Animator helmetAnimator;
    Animator bodyAnimator;

    bool meleeAttack;

    // Start is called before the first frame update
    void Start()
    {
        attack = GetComponent<Attack>();
        equipment = GetComponent<Equipment>();
        playerAnimator = GetComponent<Animator>();

        movement = GetComponent<Movement>();
        playerMovement = GetComponent<PlayerMovement>();

        mouseInfo = GetComponent<Mouse_info>();

        stats = GetComponent<CharacterStats>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (equipment.weapon != null)
        {
            weaponAnimator = GetComponent<Equipment>().weapon.GetComponent<Animator>();
        }

        moveCharacter();
        turnCharacter();
        attackCharacter();
        agitateCharacter();

        attackWeapon();
        turnWeapon();

        moveEquipment();

        if (meleeAttack == true)
        {
            playerAnimator.SetBool("IsAttacking", true);
            if (weaponAnimator != null)
            {
                weaponAnimator.SetBool("IsAttacking", true);
            }
        }
    }

    public IEnumerator MeleeAttack()
    {
        meleeAttack = true;
        yield return new WaitForSeconds(0.5f);
        meleeAttack = false;
    }

    protected override void moveCharacter()
    {

        playerAnimator.SetFloat("Horizontal", mouseInfo.mouse_direction.x);
        playerAnimator.SetFloat("Vertical", playerMovement.moveDirection.y);

        playerAnimator.SetBool("Moving", movement.isMoving);
    }

    protected override void turnCharacter()
    {
        playerAnimator.SetFloat("LastMove", mouseInfo.mouse_direction.x);
    }

    protected override void attackCharacter()
    {
        playerAnimator.SetBool("IsAttacking", attack.IsAttacking());
    }

    protected override void agitateCharacter()
    {
        playerAnimator.SetBool("IsAgitated", stats.HasBeenAgitated());
        if (weaponAnimator != null)
        {
            weaponAnimator.SetBool("IsAgitated", stats.HasBeenAgitated());
        }

    }

    public void Dash(bool dashing)
    {
        playerAnimator.SetBool("IsDashing", dashing);
    }

    void attackWeapon()
    {
        if (weaponAnimator != null)
        {
            weaponAnimator.SetBool("IsAttacking", attack.IsAttacking());
        }
    }

    void turnWeapon()
    {
        if (weaponAnimator != null)
        {
            if (playerMovement.moveDirection.x == 1f)
            {
                weaponAnimator.SetFloat("LastMove", playerMovement.moveDirection.x);

            }
            else if (playerMovement.moveDirection.x == -1f)
            {
                weaponAnimator.SetFloat("LastMove", playerMovement.moveDirection.x);

            }

        }

    }

    void moveEquipment()
    {
        if (weaponAnimator != null)
        {
            if (movement.isMoving)
            {
                weaponAnimator.SetBool("IsMoving", movement.isMoving);

            }
            else if(!movement.isMoving)
            {
                weaponAnimator.SetBool("IsMoving", movement.isMoving);

            }

        }

    }
}
