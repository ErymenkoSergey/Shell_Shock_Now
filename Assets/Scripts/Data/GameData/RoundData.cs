using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RoundData")]
public class RoundData : ScriptableObject
{
    public List<RoundInfo> RoundInfos;
}

[Serializable]
public struct RoundInfo
{
    public bool IsOpen;
    public string NameRound;
    public float RunningTime;
}