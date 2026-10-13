using UnityEngine;
using System;
public class LevelManager : MonoBehaviour
{
    public event Action LevelCompleted;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PathManager pathManager;
    [SerializeField] private WordListUI wordListUI;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelGenerator levelGenerator;
    public GeneratedLevel CurrentLevel { get; private set; }
    public int CurrentLevelIndex => GameProgress.CurrentLevel;
    private void Start()
    {
        StartLevel(CurrentLevelIndex);
    }

    public void StartLevel(int levelIndex)
    {
        GameProgress.CurrentLevel = levelIndex;
        pathManager.Clear();

        CurrentLevel = levelGenerator.Generate(levelIndex);

        gridManager.GenerateGrid(CurrentLevel);

        wordListUI.Build(CurrentLevel);
        uiManager.UpdateLevelText(CurrentLevelIndex);
    }

    public bool IsLevelComplete()
    {
        return pathManager.FoundWordCount >= CurrentLevel.Words.Count;
    }

    public void CompleteLevel()
    {
        LevelCompleted?.Invoke();
    }
}