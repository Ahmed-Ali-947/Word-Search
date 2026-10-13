using UnityEngine;

[System.Serializable]
public class WordData
{
    public string Text;

    [HideInInspector]
    public bool IsFound;

    public WordData(string text)
    {
        Text = text;
        IsFound = false;
    }
}