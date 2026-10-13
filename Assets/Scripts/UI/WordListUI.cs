using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordListUI : MonoBehaviour
{
    [SerializeField] private Transform rowParent;
    [SerializeField] private Transform rowTemplate;
    [SerializeField] private int wordsPerRow = 3;

    [SerializeField] private TMP_Text categoryText;
    [SerializeField] private WordItemUI itemPrefab;

    private readonly Dictionary<string, WordItemUI> items = new();

    public void Build(GeneratedLevel level)
    {
        foreach (Transform child in rowParent)
        {
            if (child != rowTemplate)
                Destroy(child.gameObject);
        }

        foreach (Transform child in rowTemplate)
            Destroy(child.gameObject);

        items.Clear();

        categoryText.text = level.Category;

        Transform currentRow = rowTemplate;

        int index = 0;

        foreach (WordData word in level.Words)
        {
            if (index > 0 && index % wordsPerRow == 0)
            {
                currentRow = Instantiate(rowTemplate, rowParent);

                // Remove copied word items
                for (int i = currentRow.childCount - 1; i >= 0; i--)
                {
                    Destroy(currentRow.GetChild(i).gameObject);
                }

                currentRow.gameObject.SetActive(true);
            }

            WordItemUI item =
                Instantiate(itemPrefab, currentRow);

            item.Initialize(word.Text);

            items.Add(word.Text, item);

            index++;
        }

        rowTemplate.gameObject.SetActive(true);
    }

    public void MarkWordFound(string word)
    {
        if (items.TryGetValue(word, out WordItemUI item))
        {
            item.MarkFound();
        }
    }
}