using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    { 
        StartCoroutine(ammo_death());
    }

    IEnumerator ammo_death()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
