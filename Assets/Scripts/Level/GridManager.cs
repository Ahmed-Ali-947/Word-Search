using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Cell cellPrefab;
    [Header("Grid Settings")]
    [SerializeField] private float gridPadding = 0.9f;
    [Header("Path Settings")]
    [SerializeField] private float lineWidthMultiplier = 0.65f;
    [SerializeField]
    private Vector2 gridOffset;

    public Cell[,] Cells => cells;
    public float LineWidth => CellSize * lineWidthMultiplier;
    public float CellSize { get; private set; }
    public int Width => cells.GetLength(0);
    public int Height => cells.GetLength(1);

    private Cell[,] cells;
    private Vector2 gridOrigin;
    public void GenerateGrid(GeneratedLevel level)
    {
        ClearGrid();

        int width = level.Width;
        int height = level.Height;

        cells = new Cell[width, height];

        CellSize = CalculateCellSize(width, height);

        // NEW
        char[,] letters = level.Letters;

        float gridWidth = width * CellSize;
        float gridHeight = height * CellSize;

        gridOrigin = new Vector2(
            -gridWidth * 0.5f,
            -gridHeight * 0.5f) + gridOffset;

        Vector2 bottomLeft = gridOrigin;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell = Instantiate(cellPrefab, transform);

                cell.transform.localPosition = new Vector3(
                    bottomLeft.x + (x + 0.5f) * CellSize,
                    bottomLeft.y + (y + 0.5f) * CellSize,
                    0f);

                cell.transform.localScale = Vector3.one * CellSize;

                cell.Initialize(x, y);

                // NEW
                cell.SetLetter(letters[x, y]);

                cells[x, y] = cell;
            }
        }
    }

    public Vector2Int WorldToGrid(Vector2 worldPosition)
    {
        Vector2 local = transform.InverseTransformPoint(worldPosition);

        local -= gridOrigin;

        int x = Mathf.FloorToInt(local.x / CellSize);
        int y = Mathf.FloorToInt(local.y / CellSize);

        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int coords)
    {
        Vector3 local = new Vector3(
            gridOrigin.x + (coords.x + 0.5f) * CellSize,
            gridOrigin.y + (coords.y + 0.5f) * CellSize,
            0f);
        return transform.TransformPoint(local);
    }

    public bool IsInsideGrid(Vector2Int coords)
    {
        return coords.x >= 0 &&
               coords.y >= 0 &&
               coords.x < Width &&
               coords.y < Height;
    }

    public Vector2 WorldToGridFloat(Vector2 worldPosition)
    {
        Vector2 local = transform.InverseTransformPoint(worldPosition);

        local -= gridOrigin;

        return local / CellSize;
    }

    private float CalculateCellSize(int width, int height)
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize * 2f;
        float screenWidth = screenHeight * cam.aspect;

        float horizontalSize = screenWidth / width;
        float verticalSize = screenHeight / height;

        return Mathf.Min(horizontalSize, verticalSize) * gridPadding;
    }
    public Cell GetCell(Vector2 worldPosition)
    {
        Vector2Int coords = WorldToGrid(worldPosition);

        if (!IsInsideGrid(coords))
            return null;

        return cells[coords.x, coords.y];
    }

    public Cell GetCell(Vector2Int coords)
    {
        if (!IsInsideGrid(coords))
            return null;

        return cells[coords.x, coords.y];
    }

    private void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void SetLettersVisible(bool visible)
    {
        foreach (Cell cell in cells)
        {
            if (cell != null)
                cell.SetLetterVisible(visible);
        }
    }
}