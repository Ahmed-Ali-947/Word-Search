using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Word Search/Fixed Levels",
    fileName = "FixedLevels")]
public class FixedLevelsSO : ScriptableObject
{
    public List<FixedLevelData> Levels = new();

    public FixedLevelData GetLevel(int levelNumber)
    {
        return Levels.Find(x => x.LevelNumber == levelNumber + 1);
    }
}