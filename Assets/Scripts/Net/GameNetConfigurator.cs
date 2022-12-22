using UnityEngine;
using Mirror;


    public sealed class GameNetConfigurator : CommonBehaviour, INetConfigurable
{
        //[SerializeField] private DataConfiguration _dataConfiguration;
        //private PlayerSync _playerSync;
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

    public void TransferPlayer(GameObject player)
    {
        //throw new System.NotImplementedException();
    }

    //public void TransferPlayer(GameObject player)
    //{
    //    _playerSync = player.GetComponent<PlayerSync>();
    //}

    //public Configuration GetConfiguration()
    //{
    //    return _dataConfiguration.Configuration;
    //}
}
