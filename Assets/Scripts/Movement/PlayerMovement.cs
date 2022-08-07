using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(2)]
public class PlayerMovement : Movement
{
    public Vector2 moveDirection;
    private Rigidbody2D rb;
    public float movement_Speed; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacter();
        checkLastDirection();
        
    }

    protected override void moveCharacter() {

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        if (moveDirection.x != 0f || moveDirection.y != 0f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false; ;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * movement_Speed, ForceMode2D.Force);
    }

    protected override void checkLastDirection() {
        if (moveDirection.x == 1f)
        {
            lastMove = moveDirection.x;
        }
        else if (moveDirection.x == -1f)
        {
            lastMove = moveDirection.x;
        }
    }
}
