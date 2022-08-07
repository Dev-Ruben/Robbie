using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sandstorm : MonoBehaviour
{
    public Image sandStormImage;
    // Start is called before the first frame update
    void Start()
    {
        Color tempColor = sandStormImage.color;
        tempColor.a = 1f;
        sandStormImage.color = tempColor;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
