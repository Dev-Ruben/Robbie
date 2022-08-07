using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected float movementSpeed;

    public float lastMove;

    public bool isMoving;
    protected virtual void moveCharacter() { }
    protected virtual void checkLastDirection() { }

}
