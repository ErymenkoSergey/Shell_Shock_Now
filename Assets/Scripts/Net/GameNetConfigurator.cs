using Mirror;

public sealed class GameNetConfigurator : CommonBehaviour, INetConfigurable
{
    private string _namePlayer;

    public string GetName()
    {
        return _namePlayer;
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
