using Mirror;
using UnityEngine;

public sealed class GameNetConfigurator : CommonBehaviour, INetConfigurable
{
    [SerializeField] private string _namePlayer;

    [SerializeField] private int _choosedMap;

    public string GetName()
    {
        return _namePlayer;
    }

    public void SetChoosedMap(int index)
    {
        _choosedMap = index;
        Debug.Log($"GameNetConfigurator _ {index}");
    }

    public int GetMapId()
    {
        return _choosedMap;
    }

    public void SetNamePlayer(string name)
    {
        if (name == "" || name == "Player")
        {
            name = $"Player {NetworkServer.connections.Count}";
        }

        _namePlayer = $"Player {name}";
    }
}
