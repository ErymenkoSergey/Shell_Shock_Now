using Mirror;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyConfigurator : MonoBehaviour
{
    [SerializeField] private NetworkManager _networkManager;

    [SerializeField] private Button _playGame;
    [SerializeField] private Button _createMapGame;
    [SerializeField] private Button _findGame;
    [SerializeField] private Button _server;

    [SerializeField] private GameObject _findGamePanel;
    [SerializeField] private TMP_InputField _addressServer;
    [SerializeField] private Button _connectToServer;

    private void OnEnable()
    {
        _playGame.onClick.AddListener(() => _networkManager.StartClient()); // Client
        _createMapGame.onClick.AddListener(() => _networkManager.StartHost()); //Host (Server + Client)
        _server.onClick.AddListener(() => _networkManager.StartServer()); // (Server)
        _findGame.onClick.AddListener(() => OpenFindPanel(true));
        _addressServer.onEndEdit.AddListener(SetServerAddress);
        _connectToServer.onClick.AddListener(() => OpenFindPanel(false)); 
    }

    private void OnDisable()
    {
        _playGame.onClick.RemoveAllListeners();
        _createMapGame.onClick.RemoveAllListeners();
        _findGame.onClick.RemoveAllListeners();
        _addressServer.onEndEdit.RemoveAllListeners();
        _connectToServer.onClick.RemoveAllListeners();
        _server.onClick.RemoveAllListeners();
    }

    private void OpenFindPanel(bool isOpen)
    {
        _findGamePanel.SetActive(isOpen);
    }

    private void SetServerAddress(string address)
    {
        _networkManager.networkAddress = address;
        _networkManager.StartClient();
    }
}