using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLayeringSorter : MonoBehaviour
{
    [SerializeField]
    int sortingOrderBase = 5000;
    [SerializeField]
    int offset = 0;
    [SerializeField]
    bool isStaticRender = false;

    Renderer render;

    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        render.sortingOrder = (int)(sortingOrderBase - (transform.position.y*100) - offset);
        if (isStaticRender)
        {
            Destroy(this);
        }
    }
}
