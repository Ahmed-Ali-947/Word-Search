using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private PathManager pathManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PathView hintPrefab;
    private readonly HashSet<string> hintedWords = new();

    public void ShowHint()
    {
        List<WordPlacement> candidates = new();

        foreach (WordPlacement wordPlacement in levelManager.CurrentLevel.Placements)
        {
            string word = wordPlacement.Word.Text;

            if (pathManager.HasFoundWord(word))
                continue;

            if (hintedWords.Contains(word))
                continue;

            candidates.Add(wordPlacement);
        }


        if (candidates.Count == 0)
        {
            Debug.Log("No words left to hint.");
            return;
        }

        WordPlacement placement =
            candidates[Random.Range(0, candidates.Count)];

        hintedWords.Add(placement.Word.Text);

        Cell firstCell =
            gridManager.GetCell(placement.Start);

        PathView hint =
            Instantiate(hintPrefab);

        hint.Initialize(gridManager.LineWidth);

        hint.Refresh(
            firstCell.WorldPosition,
            firstCell.WorldPosition);

        // hint.Freeze();
    }
}