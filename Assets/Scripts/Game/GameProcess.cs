using Mirror;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    [SerializeField] private InputControl _input;
    [SerializeField] private Transform _pointMap;
    [SerializeField] private GameObject _earthPrefab1;
    [SerializeField] private GameObject _earthPrefab2;
    [SerializeField] private Land _currentLand;
    [SerializeField] private GameNetConfigurator _netConfigurator;
    [SerializeField] private NetworkManager _manager;
    private Tank _worm;
    public Tank GetPlayer() => _worm;

    public GameNetConfigurator GetConfigurator() => _netConfigurator;
    public InputControl Input => _input;

    public void SetPlayer(Tank worm)
    {
        _worm = worm;
    }

    private void OnEnable()
    {
        _netConfigurator = FindObjectOfType<GameNetConfigurator>();
        _manager = FindObjectOfType<NetworkManager>();

        int choosedMap = _netConfigurator.GetMapId();
        Debug.Log($"GameNetConfigurator __ {choosedMap}");
        if (choosedMap == 1)
            CreateMap(_earthPrefab1);
        if (choosedMap == 2)
            CreateMap(_earthPrefab2);
    }

    private void CreateMap(GameObject map)
    {
        _currentLand = Instantiate(map, _pointMap).GetComponent<Land>();
    }

    public void StopGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            _manager.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            _manager.StopClient();
        }
        else if (NetworkServer.active)
        {
            _manager.StopServer();
        }
    }
}
