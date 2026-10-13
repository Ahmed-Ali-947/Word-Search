using System.Collections.Generic;
using UnityEngine;

public class BonusWordValidator : MonoBehaviour
{
    [SerializeField]
    private BonusWordDatabaseSO database;

    private readonly HashSet<string> foundBonusWords = new();

    private HashSet<string> dictionary;

    private void Awake()
    {
        dictionary = new HashSet<string>();

        foreach (WordData word in database.Words)
        {
            dictionary.Add(word.Text.ToUpper());
        }
    }

    public bool IsBonusWord(string word)
    {
        word = word.ToUpper();

        if (!dictionary.Contains(word))
            return false;

        if (foundBonusWords.Contains(word))
            return false;

        foundBonusWords.Add(word);

        return true;
    }

    public void ResetLevel()
    {
        foundBonusWords.Clear();
    }
}