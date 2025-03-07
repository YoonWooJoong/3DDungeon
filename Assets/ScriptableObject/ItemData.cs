using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Health,
    Speed,
    Jump
}

public enum EquipableType
{
    Speed,
    Jump
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}
[Serializable]
public class ItemDataEuqipStats
{
    public EquipableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public ItemDataEuqipStats[] equipableStats;
    public GameObject equipPrefab;
}
