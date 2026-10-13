using System.Text;

public static class SelectionUtility
{
    public static string BuildWord(SelectionPath path)
    {
        StringBuilder sb = new();

        foreach (Cell cell in path.Cells)
            sb.Append(cell.Letter);

        return sb.ToString();
    }
}