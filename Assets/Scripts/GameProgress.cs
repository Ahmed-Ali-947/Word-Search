using UnityEngine;

public static class GameProgress
{
    private const string LevelKey = "CurrentLevel";
    public const string Coins = "Coins";
    public const string Wisdom = "Wisdom";
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt(LevelKey, 0);

        set
        {
            PlayerPrefs.SetInt(LevelKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int CurrentWisdom
    {
        get => PlayerPrefs.GetInt(Wisdom, 0);

        set
        {
            PlayerPrefs.SetInt(Wisdom, value);
            PlayerPrefs.Save();
        }
    }

    public static void ResetProgress()
    {
        CurrentLevel = 0;
    }
}