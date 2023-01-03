using System;
using System.Collections.Generic;
using UnityEngine;
using static Mirror.NetworkRoomPlayer;

[CreateAssetMenu(menuName = "GameModeData")]
public class GameModeData : ScriptableObject
{
    public List<GameModeInfo> GameModeInfos;
}

[Serializable]
public struct GameModeInfo
{
    public bool IsOpen;
    public string NameGameMode;
    public GameMode GameModeType;
    public string DescriptionGameMode;
}