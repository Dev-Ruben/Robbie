using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobTest : MonoBehaviour
{
    Mouse_info mouse_info;
    [SerializeField] GameObject[] mobs;

    // Update is called once per frame

    private void Start()
    {
        mouse_info = GameObject.Find("Robbie").GetComponent<Mouse_info>();
    }
    void Update()
    {
        if (InputManager.Instance.getButtonDown("1"))
        {
            Instantiate(mobs[0], mouse_info.mouse_position, Quaternion.identity);
        }
        if (InputManager.Instance.getButtonDown("2"))
        {
            Instantiate(mobs[1], mouse_info.mouse_position, Quaternion.identity);
        }
        if (InputManager.Instance.getButtonDown("3"))
        {
            Instantiate(mobs[2], mouse_info.mouse_position, Quaternion.identity);
        }
        if (InputManager.Instance.getButtonDown("4"))
        {
            Instantiate(mobs[3], mouse_info.mouse_position, Quaternion.identity);
        }
        if (InputManager.Instance.getButtonDown("5"))
        {
            Instantiate(mobs[4], mouse_info.mouse_position, Quaternion.identity);
        }

    }
}
