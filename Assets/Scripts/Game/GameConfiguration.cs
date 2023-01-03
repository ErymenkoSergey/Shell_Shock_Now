using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameConfigurator", menuName = "ScriptableObjectInstaller/GameConfigurator")]
public class GameConfiguration : ScriptableObjectInstaller
{
    [SerializeField] private MainConfigurator _mainConfigurator;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private MapData _mapData;
    [SerializeField] private GameModeData _gameModeData;
    [SerializeField] private TeamData _teamData;
    [SerializeField] private RoundData _roundData;

    public override void InstallBindings()
    {
        Container.BindInstance(_mainConfigurator).AsSingle();
        Container.BindInstance(_weaponData).AsSingle();
        Container.BindInstance(_itemData).AsSingle();
        Container.BindInstance(_mapData).AsSingle();
        Container.BindInstance(_gameModeData).AsSingle();
        Container.BindInstance(_teamData).AsSingle();
        Container.BindInstance(_roundData).AsSingle();
    }
}