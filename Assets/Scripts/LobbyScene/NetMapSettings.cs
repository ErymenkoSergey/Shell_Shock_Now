using UnityEngine;
using UnityEngine.UI;
using static Mirror.NetworkRoomPlayer;
using TMPro;
using Zenject;
using System.Collections.Generic;
using Mirror;
using System.Threading.Tasks;

public class NetMapSettings : MonoBehaviour
{
    [SerializeField] private MapConfig _commonMapConfig;
    [SerializeField] private NetworkRoomPlayer _network;
    [SerializeField] private NetworkRoomPlayer[] _networks;
    [SerializeField] private NetworkRoomManager _networkManager;
    [SerializeField] private bool _isIServer;

    [Space]
    [SerializeField] private Button _leftButtonMap;
    [SerializeField] private Button _rightButtonMap;
    [SerializeField] private TextMeshProUGUI _mapNameText;
    [SerializeField] private TextMeshProUGUI _descriptionMapText;
    [SerializeField] private Image _previewMapImage;
    private int _currentMap = 0;

    [Space]
    [Inject] private MapData _mapData;
    [SerializeField] private List<MapInfo> _mapsData = new List<MapInfo>();
    [SerializeField] private int _countMaps;
    [SerializeField] private int _countCheckerMaps;

    [Space(5)]
    [SerializeField] private Button _leftButtonGameMode;
    [SerializeField] private Button _rightButtonGameMode;
    [SerializeField] private TextMeshProUGUI _gameModeNameText;
    [SerializeField] private TextMeshProUGUI _descriptionGameModeText;
    [SerializeField] private GameMode _currentGameMode;
    [SerializeField] private int _currentGameModeNumber = 0;
    [Space]
    [Inject] private GameModeData _gameModeData;
    [SerializeField] private List<GameModeInfo> _gameModeInfos = new List<GameModeInfo>();
    [SerializeField] private int _countGameMode;
    [SerializeField] private int _countCheckerGameMode;

    [Space(5)]
    [SerializeField] private Button _leftButtonTeamNumber;
    [SerializeField] private Button _rightButtonTeamNumber;
    [SerializeField] private TextMeshProUGUI _teamNameText;
    [SerializeField] private TeamNumber _teamNumber;
    [SerializeField] private int _currentTeamNumber = 0;
    [Space]
    [Inject] private TeamData _teamData;
    [SerializeField] private List<TeamInfo> _teamInfos = new List<TeamInfo>();
    [SerializeField] private int _countTeam;
    [SerializeField] private int _countCheckerTeam;

    [Space(5)]
    [SerializeField] private Button _leftButtonRoundTime;
    [SerializeField] private Button _rightButtonRoundTime;
    [SerializeField] private TextMeshProUGUI _nameRoundText;
    [SerializeField] private TextMeshProUGUI _currentRoundTimeText;
    [SerializeField] private int _currentRoundTime = 2;
    private float _roundTime;

    [Space]
    [Inject] private RoundData _roundData;
    [SerializeField] private List<RoundInfo> _roundInfos = new List<RoundInfo>();
    [SerializeField] private int _countRound;
    [SerializeField] private int _countCheckerRound;

    [SerializeField] private GameObject _uiPrefabPlayer;
    [SerializeField] private Transform _uiContent;
    [SerializeField] private Button _iReadyButton;

    [SerializeField] private GameObject _leftPanel, _centerPanel;

    private void OnEnable()
    {
        _leftButtonMap.onClick.AddListener(PreviousMap);
        _rightButtonMap.onClick.AddListener(NextMap);
        _leftButtonGameMode.onClick.AddListener(PreviousGameMode);
        _rightButtonGameMode.onClick.AddListener(NextGameMode);
        _leftButtonTeamNumber.onClick.AddListener(PreviousTeam);
        _rightButtonTeamNumber.onClick.AddListener(NextTeam);
        _leftButtonRoundTime.onClick.AddListener(PreviousRound);
        _rightButtonRoundTime.onClick.AddListener(NextRound);
        _iReadyButton.onClick.AddListener(IReadyStatus);
    }

    private void OnDisable()
    {
        _leftButtonMap.onClick.RemoveListener(PreviousMap);
        _rightButtonMap.onClick.RemoveListener(NextMap);
        _leftButtonGameMode.onClick.RemoveListener(PreviousGameMode);
        _rightButtonGameMode.onClick.RemoveListener(NextGameMode);
        _leftButtonTeamNumber.onClick.RemoveListener(PreviousTeam);
        _rightButtonTeamNumber.onClick.RemoveListener(NextTeam);
        _leftButtonRoundTime.onClick.RemoveListener(PreviousRound);
        _rightButtonRoundTime.onClick.RemoveListener(NextRound);
        _iReadyButton.onClick.RemoveListener(IReadyStatus);
    }

    private void Awake()
    {
        SetNetPoint();
    }

    private async void SetNetPoint()
    {
        OpenOptionalPanel(false);

        await Task.Delay(1000);
        if (_network == null)
        {
            _networkManager = FindObjectOfType<NetworkRoomManager>();
            //_network = _networkManager.CurrentPlayer;
        }

        if (_network != null)
        {
            _isIServer = _network.isServer;
        }

        _networks = FindObjectsOfType<NetworkRoomPlayer>();

        //if (_network.isClient)
        //{
            
        //}

        if (_isIServer == true)
        {
            OpenOptionalPanel(true);
            SetData();
        }
    }

    private void OpenOptionalPanel(bool isOpen)
    {
        _leftPanel.SetActive(isOpen);
        _centerPanel.SetActive(isOpen);
    }

    private void SetData()
    {
        _mapsData = _mapData.MapsInfo;
        _countMaps = _mapsData.Count;
        _countCheckerMaps = _countMaps - 1;

        _gameModeInfos = _gameModeData.GameModeInfos;
        _countGameMode = _gameModeInfos.Count;
        _countCheckerGameMode = _countGameMode - 1;

        _teamInfos = _teamData.TeamInfos;
        _countTeam = _teamInfos.Count;
        _countCheckerTeam = _countTeam - 1;

        _roundInfos = _roundData.RoundInfos;
        _countRound = _roundInfos.Count;
        _countCheckerRound = _countRound - 1;

        CommonUIUpdate();
    }

    private void CommonUIUpdate()
    {
        SetInfoUI();
        SetUiGameMode(_currentGameModeNumber);
        SetUiTeam(_currentTeamNumber);
        SetUiRound(_currentRoundTime);
    }

    #region Map UI
    private void PreviousMap()
    {
        if (_currentMap == 0)
        {
            _currentMap = _countCheckerMaps;
        }
        else
        {
            _currentMap -= 1;
        }

        SetInfoUI();
    }

    private void NextMap()
    {
        if (_currentMap == _countCheckerMaps)
        {
            _currentMap = 0;
        }
        else
        {
            _currentMap += 1;
        }

        SetInfoUI();
    }

    private void SetInfoUI()
    {
        _mapNameText.text = _mapsData[_currentMap].NameMap;
        _descriptionMapText.text = _mapsData[_currentMap].DescriptionMap;
        _previewMapImage.sprite = _mapsData[_currentMap].PreviewIcon;
        SetCommonConfiguration();
    }
    #endregion

    #region Game Mode
    private void PreviousGameMode()
    {
        if (_currentGameModeNumber == 0)
        {
            _currentGameModeNumber = _countCheckerGameMode;
        }
        else
        {
            _currentGameModeNumber -= 1;
        }

        SetUiGameMode(_currentGameModeNumber);
    }

    private void NextGameMode()
    {
        if (_currentGameModeNumber == _countCheckerGameMode)
        {
            _currentGameModeNumber = 0;
        }
        else
        {
            _currentGameModeNumber += 1;
        }

        SetUiGameMode(_currentGameModeNumber);
    }

    private void SetUiGameMode(int index)
    {
        _gameModeNameText.text = _gameModeInfos[index].NameGameMode;
        _descriptionGameModeText.text = _gameModeInfos[index].DescriptionGameMode;
        _currentGameMode = _gameModeInfos[index].GameModeType;
        SetCommonConfiguration();
    }
    #endregion

    #region Team
    private void PreviousTeam()
    {
        if (_currentTeamNumber == 0)
        {
            _currentTeamNumber = _countCheckerTeam;
        }
        else
        {
            _currentTeamNumber -= 1;
        }

        SetUiTeam(_currentTeamNumber);
    }

    private void NextTeam()
    {
        if (_currentTeamNumber == _countCheckerTeam)
        {
            _currentTeamNumber = 0;
        }
        else
        {
            _currentTeamNumber += 1;
        }

        SetUiTeam(_currentTeamNumber);
    }

    private void SetUiTeam(int index)
    {
        _teamNameText.text = _teamInfos[index].NameGameMode;
        _teamNumber = _teamInfos[index].TeamNumber;
        SetCommonConfiguration();
    }
    #endregion

    #region Round
    private void PreviousRound()
    {
        if (_currentRoundTime == 0)
        {
            _currentRoundTime = _countCheckerRound;
        }
        else
        {
            _currentRoundTime -= 1;
        }

        SetUiRound(_currentRoundTime);
    }

    private void NextRound()
    {
        if (_currentRoundTime == _countCheckerRound)
        {
            _currentRoundTime = 0;
        }
        else
        {
            _currentRoundTime += 1;
        }

        SetUiRound(_currentRoundTime);
    }

    private void SetUiRound(int index)
    {
        _nameRoundText.text = _roundInfos[index].NameRound;
        _currentRoundTimeText.text = _roundInfos[index].RunningTime.ToString();
        SetCommonConfiguration();
    }
    #endregion

    #region SetFullConfig
    public void SetCommonConfiguration()
    {
        _commonMapConfig.MapIndex = _currentMap;
        _commonMapConfig.GameMode = _currentGameMode;
        _commonMapConfig.Team = _teamNumber;
        _commonMapConfig.RoundTime = _currentRoundTime;

        if (_network != null)
        {
            _network.SetMapConfig(_commonMapConfig);
        }

    }
    #endregion

    private void IReadyStatus()
    {
        if (_network)
        _network.CmdChangeReadyState(true);
    }
}
