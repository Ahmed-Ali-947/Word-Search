using UnityEngine;
using TMPro;
using DG.Tweening;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private TMP_Text letterText;
    [SerializeField] private Color normalLetterColor = Color.black;
    [SerializeField] private Color selectedLetterColor = Color.yellow;
    [SerializeField] private Color foundLetterColor = Color.green;
    [SerializeField] private float lastCellScale = 1.2f;
    [SerializeField] private float scaleDuration = 0.12f;

    private Tween scaleTween;
    private bool isLastSelected;
    private Vector3 defaultScale = Vector3.one;

    public int X { get; private set; }
    public int Y { get; private set; }
    public Vector2Int Coordinates => new(X, Y);
    public Vector3 WorldPosition => transform.position;
    public char Letter { get; private set; }
    public bool IsSelected { get; private set; }

    public bool IsFound { get; private set; }

    public void SetLetter(char letter)
    {
        Letter = letter;

        letterText.text = letter.ToString();
    }
    public void Initialize(int x, int y)
    {
        X = x;
        Y = y;

        IsSelected = false;
        IsFound = false;

        isLastSelected = false;

        letterText.transform.localScale = Vector3.one;

        RefreshVisual();
    }
    public void SetSelected(bool value)
    {
        IsSelected = value;

        RefreshVisual();
    }

    public void SetFound(bool value)
    {
        IsFound = value;

        RefreshVisual();
    }

    private void RefreshVisual()
    {
        if (IsSelected)
        {
            letterText.color = selectedLetterColor;
        }
        else if (IsFound)
        {
            letterText.color = foundLetterColor;
        }
        else
        {
            letterText.color = normalLetterColor;
        }
    }
    public void SetLetterVisible(bool visible)
    {
        letterText.gameObject.SetActive(visible);
    }

    public void SetLastSelected(bool value)
    {
        if (isLastSelected == value)
            return;

        isLastSelected = value;

        scaleTween?.Kill();

        if (value)
        {
            scaleTween = letterText.transform
                .DOScale(defaultScale * lastCellScale, scaleDuration)
                .SetEase(Ease.OutBack);
        }
        else
        {
            scaleTween = letterText.transform
                .DOScale(defaultScale, scaleDuration)
                .SetEase(Ease.OutQuad);
        }
    }

}