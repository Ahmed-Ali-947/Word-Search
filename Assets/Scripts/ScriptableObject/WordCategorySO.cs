using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Word Search/Word Category")]
public class WordCategorySO : ScriptableObject
{
    public string CategoryName;

    public List<WordData> Words = new();
}