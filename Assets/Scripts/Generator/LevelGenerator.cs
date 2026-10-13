using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private WordDatabase database;
    [SerializeField] private FixedLevelsSO fixedLevels;
    [SerializeField] private LevelProgression progression;
    [SerializeField] private WordSelector selector;
    [SerializeField] private BoardGenerator boardGenerator;

    public GeneratedLevel Generate(int levelNumber)
    {
        System.Random random = new System.Random(levelNumber);
        while (true)
        {
            LevelSettings settings =
                progression.GetSettings(levelNumber);

            WordCategorySO category =
                database.GetRandomCategory(random);

            FixedLevelData fixedData =
                fixedLevels.GetLevel(levelNumber);

            List<WordData> words =
                selector.SelectWords(
                    category,
                    settings,
                    fixedData,
                    random);

            BoardGenerationResult board =
                boardGenerator.Generate(
                    words,
                    settings, random);

            if (!board.Success)
                continue;

            return new GeneratedLevel
            {
                Width = settings.Width,
                Height = settings.Height,
                Category = category.CategoryName,
                Words = words,
                Letters = board.Letters,
                Placements = board.Placements
            };
        }
    }
}