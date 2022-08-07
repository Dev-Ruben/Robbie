using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 dash_direction;
    public bool dash_active = false;
    public float dash_power;
    public float dash_cooldown;

    PlayerAnimationController animationController;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponentInParent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Change dash into event
        if(dash_active == false)
        {
            dash_direction = GetComponent<PlayerMovement>().moveDirection;
        }

        if(Input.GetMouseButtonDown(1) && dash_active == false)
        {
            rb.AddForce(dash_direction.normalized * dash_power, ForceMode2D.Impulse);
            StartCoroutine(dash_timer());
        }
        
    }

    IEnumerator dash_timer()
    {
        dash_active = true;
        animationController.Dash(dash_active); 
        yield return new WaitForSeconds(dash_cooldown);
        dash_active = false;
        animationController.Dash(dash_active); 
    }

}
