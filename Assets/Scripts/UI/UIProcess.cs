using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Zenject;

public class UIProcess : MonoBehaviour
{
    [Inject] public WeaponData _weaponsData;
    [Inject] public ItemData _itemData;

    [SerializeField] private GameProcess _gameProcess;
    [SerializeField] private Button _leaveGameButton;
    [SerializeField] private Button _fireButton;
    [SerializeField] private Button _weaponTypeButton;
    [SerializeField] private Button _weaponTypeClousedPanelButton;
    [SerializeField] private Button _itemButton;
    [SerializeField] private Button _itemClousedPanelButton;

    [SerializeField] private GameObject _weaponPanel;
    [SerializeField] private GameObject _weaponItemPrefab;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _contentWeapon;
    [SerializeField] private Transform _contentItem;
    [SerializeField] private Image _currentWeaponIcon;
    [SerializeField] private TextMeshProUGUI _currentWeaponName;

    [SerializeField] private GameObject _itemPanel;

    [SerializeField] private TextMeshProUGUI _moveNow;
    [SerializeField] private TextMeshProUGUI _timer;

    private List<Weapon> _Weapons = new List<Weapon>();
    private List<ItemInfo> _items = new List<ItemInfo>();
    [SerializeField] private int _currentGun;
    [SerializeField] private int _currentItem;

    private Tank _player;

    private void OnEnable()
    {
        _weaponTypeButton.onClick.AddListener(() => OpenWeaponPanel(true));
        _weaponTypeClousedPanelButton.onClick.AddListener(() => OpenWeaponPanel(false));
        _itemButton.onClick.AddListener(() => OpenItemPanel(true));
        _itemClousedPanelButton.onClick.AddListener(() => OpenItemPanel(false));
        _fireButton.onClick.AddListener(Fire);
        _leaveGameButton.onClick.AddListener(LeaveGame);
    }

    private void OnDisable()
    {
        _weaponTypeButton.onClick.RemoveListener(() => OpenWeaponPanel(true));
        _weaponTypeClousedPanelButton.onClick.RemoveListener(() => OpenWeaponPanel(false));
        _itemButton.onClick.RemoveListener(() => OpenItemPanel(true));
        _itemClousedPanelButton.onClick.AddListener(() => OpenItemPanel(false));
        _fireButton.onClick.RemoveListener(Fire);
        _leaveGameButton.onClick.RemoveListener(LeaveGame);
    }

    private void Start()
    {
        SetWeaponsData();
    }

    private void SetWeaponsData()
    {
        _Weapons.Clear();

        _Weapons = _weaponsData.Weapons;
        _items = _itemData.Items;
        InstantiateCurrentWeaponType(_Weapons);
        InstantiateCurrentItem(_items);
    }

    private void OpenWeaponPanel(bool isOpen)
    {
        _weaponPanel.SetActive(isOpen);
    }

    private void OpenItemPanel(bool isOpen)
    {
        _itemPanel.SetActive(isOpen);
    }

    public void InstantiateCurrentWeaponType(List<Weapon> Weapons)
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Instantiate(_weaponItemPrefab, _contentWeapon).GetComponent<WeaponItem>().SetData(i, Weapons[i], this);
        }
    }

    public void InstantiateCurrentItem(List<ItemInfo> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            Instantiate(_itemPrefab, _contentItem).GetComponent<Item>().SetData(i, item[i], this);
        }
    }

    public void SetChangeWeapon(int index)
    {
        OpenWeaponPanel(false);
        _currentGun = index;
        SetCurrentWeaponInfo();
    }

    public void SetChangeItem(int index)
    {
        OpenItemPanel(false);
        _currentItem = index;

    }

    private void SetCurrentWeaponInfo()
    {
        _currentWeaponIcon.sprite = _Weapons[_currentGun].Icon;
        _currentWeaponName.text = _Weapons[_currentGun].Name;
    }

    private void Fire()
    {
        _player = _gameProcess.GetPlayer();
        _player.FireGun(_currentGun);
    }

    protected void LeaveGame()
    {
        _gameProcess.StopGame();
    }
}
