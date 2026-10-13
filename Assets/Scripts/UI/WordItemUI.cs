using TMPro;
using UnityEngine;

public class WordItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    public string Word { get; private set; }

    public void Initialize(string word)
    {
        Word = word;

        label.text = word;

    }

    public void MarkFound()
    {
        label.color = Color.gray;
    }
}