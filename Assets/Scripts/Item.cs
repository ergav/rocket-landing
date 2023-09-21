using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/item")]
public class Item : ScriptableObject
{
    public ItemType itemType;

    public float healValue = 25;
}

public enum ItemType
{
    Health,
    Fuel
}