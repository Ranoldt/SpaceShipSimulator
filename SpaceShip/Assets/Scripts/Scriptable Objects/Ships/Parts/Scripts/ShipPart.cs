using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : ScriptableObject
{
    public Sprite icon { get { return _icon; } private set { _icon = value; } }
    [SerializeField]
    private Sprite _icon;
}
