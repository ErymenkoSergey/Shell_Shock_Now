using UnityEngine;
using UnityEngine.UI;
using static Mirror.NetworkRoomPlayer;
using TMPro;
using Zenject;
using System.Collections.Generic;

public class NetMapSettings : MonoBehaviour
{
    [Space]
    [SerializeField] private Button _leftButtonMap;
    [SerializeField] private Button _rightButtonMap;
    [SerializeField] private TextMeshProUGUI _mapNameText;
    [SerializeField] private TextMeshProUGUI _descriptionMapText;
    [SerializeField] private Image _previewMapImage;
    private int _currentMap = 0;

    [Space]
    [Inject] private MapData _mapData;
    private List<MapInfo> _mapsData = new List<MapInfo>();
    [SerializeField] private int _countMaps;
    [SerializeField] private int _countCheckerMaps;

    [Space(5)]
    [SerializeField] private Button _leftButtonGameMode;
    [SerializeField] private Button _rightButtonGameMode;
    [SerializeField] private TextMeshProUGUI _gameModeNameText;
    [SerializeField] private TextMeshProUGUI _descriptionGameModeText;
    private GameMode _currentGameMode;
    private int _currentGameModeNumber = 0;
    [Space]
    [Inject] private GameModeData _gameModeData;
    private List<GameModeInfo> _gameModeInfos = new List<GameModeInfo>();
    [SerializeField] private int _countGameMode;
    [SerializeField] private int _countCheckerGameMode;

    [Space(5)]
    [SerializeField] private Button _leftButtonTeamNumber;
    [SerializeField] private Button _rightButtonTeamNumber;
    [SerializeField] private TextMeshProUGUI _teamNameText;
    private TeamNumber _teamNumber;
    private int _currentTeamNumber = 0;
    [Space]
    [Inject] private TeamData _teamData;
    private List<TeamInfo> _teamInfos = new List<TeamInfo>();
    [SerializeField] private int _countTeam;
    [SerializeField] private int _countCheckerTeam;


    [Space(5)]
    [SerializeField] private Button _leftButtonRoundTime;
    [SerializeField] private Button _rightButtonRoundTime;
    [SerializeField] private TextMeshProUGUI _nameRoundText;
    [SerializeField] private TextMeshProUGUI _currentRoundTimeText;
    private int _currentRoundTime = 0;
    private float _roundTime;

    [Space]
    [Inject] private RoundData _roundData;
    private List<RoundInfo> _roundInfos = new List<RoundInfo>();
    [SerializeField] private int _countRound;
    [SerializeField] private int _countCheckerRound;

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
    }

    private void Awake()
    {
        SetData();
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

        SetMapChoose(_currentMap);
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

        SetMapChoose(_currentMap);
    }

    private void SetMapChoose(int CurrentMap)
    {
        SetInfoUI();
    }

    private void SetInfoUI()
    {
        _mapNameText.text = _mapsData[_currentMap].NameMap;
        _descriptionMapText.text = _mapsData[_currentMap].DescriptionMap;
        _previewMapImage.sprite = _mapsData[_currentMap].PreviewIcon;
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
    }
    #endregion
}
