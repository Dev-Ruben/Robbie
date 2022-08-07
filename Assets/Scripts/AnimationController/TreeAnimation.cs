using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    Animator animator;

    float windBlow;
    public float windTimer;
    float mapSizeX, mapSizeY;

    bool blowWind;

    void Start()
    {
        animator = GetComponent<Animator>();

        mapSizeX = GameObject.Find("MapCreator").GetComponent<MapGenerator>().GetMapSize().x;
        mapSizeY = GameObject.Find("MapCreator").GetComponent<MapGenerator>().GetMapSize().y;

        windBlow = mapSizeX;

        windTimer = transform.position.x;
        windTimer -= transform.position.y;

        StartCoroutine(WindTimer());
    }
    void Update()
    {
        if(blowWind)
        {
            animator.SetBool("HasWind", blowWind);
        }
        else
        {
            windTimer += 1f * Time.deltaTime;
        }
    }

    void StopBlowing() { 
        blowWind = false; 
        windTimer = 0;
        animator.SetBool("HasWind", blowWind);
    }

    IEnumerator WindTimer()
    {
        Debug.Log("waiting for the wind to blow");
        yield return new WaitUntil( () => windTimer >= windBlow);
        blowWind = true;
        yield return new WaitForSeconds(0.5f);
        blowWind = false;
        StopBlowing();
        StartCoroutine(WindTimer());
    }
}