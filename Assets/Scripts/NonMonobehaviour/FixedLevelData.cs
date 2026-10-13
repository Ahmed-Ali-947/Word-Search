using System;
using System.Collections.Generic;

[Serializable]
public class FixedLevelData
{
    public int LevelNumber;

    public List<WordData> RequiredWords = new();
}