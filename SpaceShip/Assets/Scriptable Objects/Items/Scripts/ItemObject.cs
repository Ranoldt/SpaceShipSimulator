using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    ore,
    heal,
    upgrade
}
public class ItemObject : ScriptableObject
{
    public Sprite icon;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;

}