using System.Collections.Generic;
using UnityEngine;

public class BonusWordUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private WordItemUI itemPrefab;

    private readonly HashSet<string> displayedWords = new();

    public void AddWord(string word)
    {
        word = word.ToUpper();

        if (!displayedWords.Add(word))
            return;

        WordItemUI item =
            Instantiate(itemPrefab, container);

        item.Initialize(word);
    }

    public void Clear()
    {
        displayedWords.Clear();

        for (int i = container.childCount - 1; i >= 0; i--)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
}