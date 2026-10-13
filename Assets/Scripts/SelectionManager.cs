using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PathManager pathManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private PathView pathPrefab;
    [SerializeField] private WordValidator validator;
    [SerializeField] private BonusWordValidator bonusValidator;
    [SerializeField] private WordListUI wordListUI;
    [SerializeField] private BonusWordUI bonusWordUI;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private float dragThreshold = 0.35f;

    private Vector2 pointerDownWorldPos;
    private bool dragStarted;
    private readonly SelectionPath activePath = new();
    private readonly List<Cell> selectedCells = new();
    private PathView activeView;
    private bool isSelecting;
    private Vector2Int startCoords;
    private Cell previousLastCell;

    public event Action<SelectionPath> SelectionStarted;
    public event Action<SelectionPath> SelectionUpdated;
    public event Action<SelectionPath> SelectionEnded;
    private bool inputEnabled = true;

    private void OnEnable()
    {
        inputManager.PointerDown += PointerDown;
        inputManager.PointerDragged += PointerDragged;
        inputManager.PointerUp += PointerUp;
    }

    private void OnDisable()
    {
        inputManager.PointerDown -= PointerDown;
        inputManager.PointerDragged -= PointerDragged;
        inputManager.PointerUp -= PointerUp;
    }

    private void PointerDown(Vector2 worldPos)
    {
        if (!inputEnabled)
            return;

        Cell cell = gridManager.GetCell(worldPos);

        if (cell == null)
            return;

        pointerDownWorldPos = worldPos;
        dragStarted = false;

        isSelecting = true;

        startCoords = cell.Coordinates;

        activePath.Clear();
        activePath.Cells.Add(cell);
        RefreshHighlights();

        activeView = Instantiate(pathPrefab);

        activeView.Initialize(gridManager.LineWidth);

        activeView.Refresh(
            activePath.First.WorldPosition,
            activePath.Last.WorldPosition);

        SelectionStarted?.Invoke(activePath);
    }
    private void PointerDragged(Vector2 worldPos)
    {
        if (!inputEnabled)
            return;

        if (!isSelecting)
            return;

        if (!dragStarted)
        {
            Vector2 gridDelta =
                gridManager.WorldToGridFloat(worldPos) -
                gridManager.WorldToGridFloat(pointerDownWorldPos);

            if (gridDelta.magnitude < dragThreshold)
                return;

            dragStarted = true;
        }

        RecalculatePath(worldPos);
        UpdateLastCellAnimation();
        RefreshHighlights();

        activeView.Refresh(
            activePath.First.WorldPosition,
            activePath.Last.WorldPosition);
    }

    private void RecalculatePath(Vector2 pointerWorld)
    {
        activePath.Clear();

        Cell startCell = gridManager.GetCell(startCoords);

        activePath.Cells.Add(startCell);

        Vector2 pointerGrid = gridManager.WorldToGridFloat(pointerWorld);

        Vector2 delta = pointerGrid - (Vector2)startCoords;

        if (delta.sqrMagnitude < 0.01f)
            return;

        Vector2Int direction = SnapDirectionFromAngle(delta);

        int distance = CalculateDistance(delta, direction);

        for (int i = 1; i <= distance; i++)
        {
            Vector2Int coords = startCoords + direction * i;

            Cell cell = gridManager.GetCell(coords);

            if (cell == null)
                break;

            activePath.Cells.Add(cell);
        }

        SelectionUpdated?.Invoke(activePath);
    }

    private void UpdateLastCellAnimation()
    {
        if (previousLastCell != null)
            previousLastCell.SetLastSelected(false);

        if (activePath.Count == 0)
        {
            previousLastCell = null;
            return;
        }

        previousLastCell = activePath.Last;

        previousLastCell.SetLastSelected(true);
    }
    private Vector2Int SnapDirectionFromAngle(Vector2 delta)
    {
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360f;

        int sector = Mathf.RoundToInt(angle / 45f) % 8;

        return sector switch
        {
            0 => Vector2Int.right,
            1 => new Vector2Int(1, 1),
            2 => Vector2Int.up,
            3 => new Vector2Int(-1, 1),
            4 => Vector2Int.left,
            5 => new Vector2Int(-1, -1),
            6 => Vector2Int.down,
            _ => new Vector2Int(1, -1)
        };
    }

    private int CalculateDistance(Vector2 delta, Vector2Int direction)
    {
        float distance;

        if (direction.x == 0)
        {
            distance = Mathf.Abs(delta.y);
        }
        else if (direction.y == 0)
        {
            distance = Mathf.Abs(delta.x);
        }
        else
        {
            distance = Mathf.Min(
                Mathf.Abs(delta.x),
                Mathf.Abs(delta.y));
        }

        return Mathf.FloorToInt(distance + 0.5f);
    }

    private void RefreshHighlights()
    {
        ClearHighlights();

        foreach (Cell cell in activePath.Cells)
        {
            cell.SetSelected(true);

            selectedCells.Add(cell);
        }
    }
    private void ClearHighlights()
    {
        foreach (Cell cell in selectedCells)
        {
            cell.SetSelected(false);
        }

        selectedCells.Clear();
    }

    private void PointerUp(Vector2 worldPos)
    {
        if (!inputEnabled)
            return;

        if (!isSelecting)
            return;

        isSelecting = false;

        FinishSelection();

        activeView = null;

        activePath.Clear();

        SelectionEnded?.Invoke(activePath);

        if (previousLastCell != null)
        {
            previousLastCell.SetLastSelected(false);
            previousLastCell = null;
        }
    }

    private void FinishSelection()
    {
        bool keepPath = false;

        if (activePath.Count > 1)
        {
            string word = SelectionUtility.BuildWord(activePath);

            if (pathManager.HasFoundWord(word))
            {
                Debug.Log($"ALREADY FOUND : {word}");
            }
            else if (validator.IsWordInLevel(word))
            {
                keepPath = true;

                // activeView.Freeze();

                pathManager.Register(
                    word,
                    activePath.Clone(),
                    activeView);
                wordListUI.MarkWordFound(word);

                foreach (Cell cell in activePath.Cells)
                {
                    cell.SetFound(true);
                }

                ClearHighlights();

                selectedCells.Clear();

                Debug.Log($"VALID : {word}");

                if (levelManager.IsLevelComplete())
                {
                    levelManager.CompleteLevel();
                }
            }
            else if (bonusValidator.IsBonusWord(word))
            {
                Destroy(activeView.gameObject);

                bonusWordUI.AddWord(word);
                coinManager.AddCoins(20);

                Debug.Log($"BONUS WORD: {word}");
                Debug.Log("+20 Coins");
            }
            else
            {
                Debug.Log($"INVALID : {word}");
            }
        }

        if (!keepPath && activeView != null)
        {
            Destroy(activeView.gameObject);
        }
        if (!keepPath)
        {
            ClearHighlights();
        }
    }

    public void SetInputEnabled(bool enabled)
    {
        inputEnabled = enabled;
    }
}