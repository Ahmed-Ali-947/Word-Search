using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    public LevelSettings GetSettings(int level)
    {
        LevelSettings settings = new();

        settings.Width = 7 + level / 10;
        settings.Height = settings.Width;

        settings.WordCount = Mathf.Min(4 + level / 5, 10);

        settings.MinWordLength = 3;

        settings.MaxWordLength = Mathf.Clamp(
            4 + level / 6,
            4,
            settings.Width);

        settings.AllowDiagonal = level >= 5;

        settings.AllowBackwards = level >= 20;

        return settings;
    }
}