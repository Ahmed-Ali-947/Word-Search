using System.Collections.Generic;
using UnityEngine;

public class WordSelector : MonoBehaviour
{
    public List<WordData> SelectWords(
        WordCategorySO category,
        LevelSettings settings,
        FixedLevelData fixedData,
        System.Random random)
    {
        List<WordData> candidates = GetCandidates(category, settings);

        List<WordData> result = new();

        // Add required words first
        if (fixedData != null &&
        fixedData.RequiredWords.Count > settings.WordCount)
        {
            Debug.LogError(
                $"Level {fixedData.LevelNumber} contains more required words than allowed by LevelSettings.");

            return new List<WordData>();
        }

        if (fixedData != null)
        {
            foreach (WordData word in fixedData.RequiredWords)
            {
                result.Add(Clone(word));

                candidates.RemoveAll(x => x.Text == word.Text);
            }
        }

        // Make sure enough words remain

        if (result.Count + candidates.Count < settings.WordCount)
        {
            Debug.LogError(
                $"Category '{category.CategoryName}' doesn't contain enough words!");

            return new List<WordData>();
        }

        // Prefer longer words

        candidates.Sort((a, b) => b.Text.Length.CompareTo(a.Text.Length));

        int remainingNeeded = settings.WordCount - result.Count;

        int longestCount = Mathf.Max(1, remainingNeeded / 3);

        for (int i = 0;
             i < longestCount && candidates.Count > 0;
             i++)
        {
            result.Add(Clone(candidates[0]));
            candidates.RemoveAt(0);
        }

        // Shuffle remaining

        Shuffle(candidates, random);

        while (result.Count < settings.WordCount &&
               candidates.Count > 0)
        {
            result.Add(Clone(candidates[0]));
            candidates.RemoveAt(0);
        }

        return result;
    }

    private List<WordData> GetCandidates(
        WordCategorySO category,
        LevelSettings settings)
    {
        List<WordData> result = new();

        foreach (WordData word in category.Words)
        {
            int length = word.Text.Length;

            if (length < settings.MinWordLength)
                continue;

            if (length > settings.MaxWordLength)
                continue;

            result.Add(word);
        }

        return result;
    }

    private WordData Clone(WordData word)
    {
        return new WordData(word.Text);
    }

    private void Shuffle(
        List<WordData> list,
        System.Random random)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);

            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}