using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    public List<ItemInfo> Items;
}

[Serializable]
public struct ItemInfo
{
    public bool IsOpen;
    public string Name;
    public int CurrentLevelUpgrade;
    public Sprite Icon;
    public float Damage;
}