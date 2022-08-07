using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobMovement : Movement
{
    protected MobAlertRadius detector;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = gameObject.GetComponent<MobStats>().getSpeed().GetValue();
        detector = gameObject.transform.GetChild(1).GetComponent<MobAlertRadius>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCharacter();
        checkLastDirection();
    }

    protected override void moveCharacter() { }

    protected override void checkLastDirection() {
        float currentPosition = gameObject.transform.position.x;
        //float playerPosition = detector.GetPlayer().transform.position.x;

        //lastMove = currentPosition < playerPosition ? 1 : -1;        
     }
}
