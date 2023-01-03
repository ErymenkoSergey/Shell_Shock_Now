using System;
using System.Collections.Generic;
using UnityEngine;
using static Mirror.NetworkRoomPlayer;

[CreateAssetMenu(menuName = "TeamData")]
public class TeamData : ScriptableObject
{
    public List<TeamInfo> TeamInfos;
}

[Serializable]
public struct TeamInfo
{
    public bool IsOpen;
    public string NameGameMode;
    public TeamNumber TeamNumber;
    public Sprite IconTeam;
}