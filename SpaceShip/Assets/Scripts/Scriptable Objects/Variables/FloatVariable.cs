using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    [TextArea] public string developerDescription;

    public float FloatValue;

    public void SetValue(float value)
    {
        FloatValue = value;
    }

    public void IncrementValue(float value)
    {
        FloatValue += value;
    }
    public void DecrementValue(float value)
    {
        FloatValue -= value;
    }
}
