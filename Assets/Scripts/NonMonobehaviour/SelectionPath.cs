using System.Collections.Generic;

public class SelectionPath
{
    public readonly List<Cell> Cells = new();

    public int Count => Cells.Count;

    public Cell First => Count > 0 ? Cells[0] : null;

    public Cell Last => Count > 0 ? Cells[^1] : null;

    public void Clear()
    {
        Cells.Clear();
    }

    public SelectionPath Clone()
    {
        SelectionPath clone = new();

        clone.Cells.AddRange(Cells);

        return clone;
    }
}