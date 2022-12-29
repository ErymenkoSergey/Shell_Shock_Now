using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private Image _iconWeapon;
    [SerializeField] private TextMeshProUGUI _nameWeapon; 
    [SerializeField] private TextMeshProUGUI _levelUpgradeWeapon; 
    [SerializeField] private TextMeshProUGUI _damageWeapon;
    [SerializeField] private Button _button;
    private UIProcess _uIProcess;
    private int _index;

    private void OnEnable()
    {
        _button.onClick.AddListener(ClickThisWeapon);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ClickThisWeapon);
    }

    private void ClickThisWeapon()
    {
        _uIProcess.SetChangeWeapon(_index);
    }

    public void SetData(int id, Weapon weapon, UIProcess uI)
    {
        _index = id;
        _iconWeapon.sprite = weapon.Icon;
        _nameWeapon.text = weapon.Name;
        _levelUpgradeWeapon.text = weapon.CurrentLevelUpgrade.ToString();
        _damageWeapon.text = weapon.Damage.ToString();
        _uIProcess = uI;
    }
}
