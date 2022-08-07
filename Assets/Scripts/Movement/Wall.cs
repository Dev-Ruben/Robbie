using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Rigidbody2D characterBody;

    private void OnCollisionStay2D(Collision2D character)
    {
        //Debug.Log(character.transform.parent.name);
        if(character.transform.tag == "PlayerBody" || character.transform.tag == "EnemyBody")
        {
            characterBody = character.collider.GetComponentInParent<Rigidbody2D>();
            characterBody.velocity = Vector3.zero;

            Debug.Log(characterBody.velocity);
            Debug.Log(character.transform.name);
        }
    }
}
