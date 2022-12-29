using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Zenject;

public class UIProcess : MonoBehaviour
{
    [Inject] public WeaponData _weaponsData;
    [SerializeField] private GameProcess _gameProcess;
    [SerializeField] private Button _leaveGameButton;
    [SerializeField] private Button _fireButton;
    [SerializeField] private Button _weaponTypeButton;
    [SerializeField] private Button _itemButton;

    [SerializeField] private GameObject _weaponPanel;
    [SerializeField] private GameObject _weaponItemPrefab;
    [SerializeField] private Transform _contentWeapon;
    [SerializeField] private Image _currentWeaponIcon;
    [SerializeField] private TextMeshProUGUI _currentWeaponName;

    [SerializeField] private GameObject _itemPanel;

    [SerializeField] private TextMeshProUGUI _moveNow;
    [SerializeField] private TextMeshProUGUI _timer;

    private List<Weapon> _Weapons = new List<Weapon>();
    [SerializeField] private int _currentGun;

    private Worm _player;

    private void OnEnable()
    {
        _weaponTypeButton.onClick.AddListener(() => OpenWeaponPanel(true));
        _fireButton.onClick.AddListener(Fire);
    }

    private void OnDisable()
    {
        _weaponTypeButton.onClick.RemoveListener(() => OpenWeaponPanel(true));
        _fireButton.onClick.RemoveListener(Fire);
    }

    private void Start()
    {
        SetWeaponsData();
    }

    private void SetWeaponsData()
    {
        _Weapons.Clear();

        _Weapons = _weaponsData.Weapons;
        InstantiateCurrentWeaponType(_Weapons);
        
    }

    private void OpenWeaponPanel(bool isOpen)
    {
        _weaponPanel.SetActive(isOpen);
    }

    public void InstantiateCurrentWeaponType(List<Weapon> Weapons)
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Instantiate(_weaponItemPrefab, _contentWeapon).GetComponent<WeaponItem>().SetData(i, Weapons[i], this);
        }
    }

    public void SetChangeWeapon(int index)
    {
        OpenWeaponPanel(false);
        _currentGun = index;
        SetCurrentWeaponInfo();
    }

    private void SetCurrentWeaponInfo()
    {
        _currentWeaponIcon.sprite = _Weapons[_currentGun].Icon;
        _currentWeaponName.text = _Weapons[_currentGun].Name;
    }

    private void Fire()
    {
        _player = _gameProcess.GetPlayer();
        _player.Fire(_Weapons[_currentGun].Prefab);
    }
}
