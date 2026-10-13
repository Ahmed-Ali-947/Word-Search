using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    menuName = "Word Search/Bonus Word Database",
    fileName = "BonusWordDatabase")]
public class BonusWordDatabaseSO : ScriptableObject
{
    public List<WordData> Words = new();
}