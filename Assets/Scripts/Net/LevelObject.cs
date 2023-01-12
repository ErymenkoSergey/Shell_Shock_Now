using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_", menuName = "Data/Level Data", order = 0)]
public class LevelObject : ScriptableObject
{
    [Mirror.Scene] public string levelName;
}