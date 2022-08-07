using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_info : MonoBehaviour
{

    public Vector2 mouse_position;
    public Vector2 mouse_direction;

    void Update()
    {
        mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);    

        mouse_direction = (mouse_position - new Vector2(transform.position.x, transform.position.y)).normalized;
    }
}


