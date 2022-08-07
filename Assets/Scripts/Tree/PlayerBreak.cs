using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBreak : MonoBehaviour
{
    int breakdamage = 1;
    float actionRadius = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BreakObject();
    }

    void BreakObject()
    {
        if (InputManager.Instance.getButtonDown("Attack"))
        {
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, actionRadius);

            foreach (Collider2D collider in collider2DArray)
            {
                IBreakable breakable = collider.GetComponent<IBreakable>();

                if (breakable != null)
                {
                    breakable.Break(breakdamage);
                }
            }

        }
    }
}
