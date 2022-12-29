using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    public List<Weapon> Weapons;
}

[Serializable]
public struct Weapon
{
    public bool IsOpen;
    public string Name;
    public int CurrentLevelUpgrade;
    public Sprite Icon;
    public GameObject Prefab;
    public float Damage;
    public float Speed;
    public float Radius;
    public bool IsDivided;
    public int CountDivided;
}