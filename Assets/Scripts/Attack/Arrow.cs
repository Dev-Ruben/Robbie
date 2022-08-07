using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 flight_directon;
    public float flight_speed = 0.3f;
    public float lifeTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flight_directon = GameObject.Find("Robbie").GetComponent<Mouse_info>().mouse_direction;
        rb.AddForce(flight_directon.normalized * flight_speed, ForceMode2D.Impulse);
        StartCoroutine(ammo_death());
    }

    IEnumerator ammo_death()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
