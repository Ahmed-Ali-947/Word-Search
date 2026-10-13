using UnityEngine;

[System.Serializable]
public class LevelSettings
{
    public int Width;
    public int Height;

    public int WordCount;
    public int BonusWordCount;

    public int MinWordLength;
    public int MaxWordLength;

    public bool AllowDiagonal;
    public bool AllowBackwards;
}