using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverItem : MonoBehaviour
{
    private Animation hoverAnimation;

    // Start is called before the first frame update
    void Start()
    {
        hoverAnimation = GetComponent<Animation>();
    }

    public void Play()
    {
        hoverAnimation.Play();
    }
    public void Stop()
    {
        hoverAnimation.Stop();
    }
}
