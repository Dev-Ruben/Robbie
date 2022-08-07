using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;

    public float GetValue() {
        return baseValue;
    }

    public void SetValue(float value) {
        baseValue = value;
    }

    public void AddValue(float value)
    {
        baseValue += value;
    }

    public void ResetValue()
    {
        baseValue = 0f;
        Debug.Log(baseValue);
    }
}


