using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        transform.position = Input.mousePosition;
        SetCustomCursor();
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetCustomCursor(Sprite itemImage = null)
    {
        SetCustomCursorImage(itemImage);
    }

    private void SetCustomCursorImage(Sprite itemSprite)
    {
        if (itemSprite != null)
        {
            image.sprite = itemSprite;
            image.enabled = true;
        }
        else 
        {
            image.sprite = null;
            image.enabled = false;
        }
    }

}
