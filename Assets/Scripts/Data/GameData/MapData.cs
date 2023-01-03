using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MapData")]
public class MapData : ScriptableObject
{
    public List<MapInfo> MapsInfo;
}

[Serializable]
public struct MapInfo
{
    public bool IsOpen;
    public string NameMap;
    public string DescriptionMap;
    public Sprite PreviewIcon;
    public GameObject MapPrefab;
}