using System.Collections.Generic;
using UnityEngine;

public class WordDatabase : MonoBehaviour
{
    [SerializeField]
    private List<WordCategorySO> categories = new();

    public IReadOnlyList<WordCategorySO> Categories => categories;

    public WordCategorySO GetRandomCategory(System.Random random)
    {
        return categories[
            random.Next(categories.Count)];
    }
}