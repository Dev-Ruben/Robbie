using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layering : MonoBehaviour
{
    public void SetLayer(GameObject gameObject, float layerHeight)
    {
        layerHeight *= 100;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)-layerHeight;
    }
}
