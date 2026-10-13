using UnityEngine;

public class WordValidator : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    public bool IsWordInLevel(string word)
    {
        foreach (WordData wordData in levelManager.CurrentLevel.Words)
        {
            if (wordData.Text == word)
                return true;
        }

        return false;
    }
}