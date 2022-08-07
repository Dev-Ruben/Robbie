using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private float actionRadius = 0.01f;

    private int currentActionID;

    private void Update()
    {
        useActionInput();
    }

    public int GetCurrentActionID()
    {
        return currentActionID;
    }
    public void SetCurrentActionID(int id)
    {
        currentActionID = id;
    }

    public void useActionInput() {
        if (InputManager.Instance.getButtonDown("Action")) {

            
            Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, actionRadius);

            foreach (Collider2D collider in collider2DArray) {
                IActionable actionable = collider.GetComponent<IActionable>();

                if (actionable != null)
                {
                    actionable.UseAction();
                }
            }
        }
    }
}
