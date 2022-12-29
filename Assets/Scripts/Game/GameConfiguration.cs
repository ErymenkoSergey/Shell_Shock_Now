using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigurator", menuName = "ScriptableObjectInstaller/GameConfigurator")]
public class GameConfiguration : ScriptableObjectInstaller
{
    [SerializeField] private MainConfigurator _mainConfigurator;
    [SerializeField] private WeaponData _weaponData;

    public override void InstallBindings()
    {
        Container.BindInstance(_mainConfigurator).AsSingle();
        Container.BindInstance(_weaponData).AsSingle();
    }
}