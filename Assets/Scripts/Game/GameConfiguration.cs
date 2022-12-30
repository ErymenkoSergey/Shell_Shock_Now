using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigurator", menuName = "ScriptableObjectInstaller/GameConfigurator")]
public class GameConfiguration : ScriptableObjectInstaller
{
    [SerializeField] private MainConfigurator _mainConfigurator;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private ItemData _itemData;

    public override void InstallBindings()
    {
        Container.BindInstance(_mainConfigurator).AsSingle();
        Container.BindInstance(_weaponData).AsSingle();
        Container.BindInstance(_itemData).AsSingle();
    }
}