using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "WeaponData")]
public class WeaponData : ScriptableObject
{
    //[SerializeField] private List<Weapon> _weaponData;
    public List<Weapon> Weapons;// => _weaponData;
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