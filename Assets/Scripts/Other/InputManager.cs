using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingletonMonobehaviour<InputManager>
{
    Dictionary<string, KeyCode> buttonKeys;

    void Start()
    {
        buttonKeys = new Dictionary<string, KeyCode>
        {
            ["Walk"] = KeyCode.W,
            ["Walk Left"] = KeyCode.A,
            ["Walk Down"] = KeyCode.S,
            ["Walk Right"] = KeyCode.D,

            ["Attack"] = KeyCode.Mouse0,
            ["Action"] = KeyCode.E,

            ["Map"] = KeyCode.M,
            ["Camera"] = KeyCode.C,
            ["Camera2"] = KeyCode.P,

            ["Inventory"] = KeyCode.I,
            ["Chest"] = KeyCode.U,
            ["Flip Panel"] = KeyCode.F,
            ["Weapon Swap"] = KeyCode.Tab,
            ["Drop"] = KeyCode.Q,

            ["Escape"] = KeyCode.Escape,

            ["1"] = KeyCode.Alpha1,
            ["2"] = KeyCode.Alpha2,
            ["3"] = KeyCode.Alpha3,
            ["4"] = KeyCode.Alpha4,
            ["5"] = KeyCode.Alpha5,
            ["6"] = KeyCode.Alpha6,
            ["7"] = KeyCode.Alpha7,
            ["8"] = KeyCode.Alpha8,
        };
    }

    public bool getButtonDown(string buttonName) {
        if (buttonKeys.ContainsKey(buttonName) == false) {
            return false;
        }

        return Input.GetKeyDown(buttonKeys[buttonName]);

    }

    public bool getButtonUp(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            return false;
        }

        return Input.GetKeyUp(buttonKeys[buttonName]);

    }
}
