using UnityEngine;

public class SelectionDebugger : MonoBehaviour
{
    [SerializeField] private SelectionManager manager;

    private void OnEnable()
    {
        manager.SelectionUpdated += Print;
    }

    private void OnDisable()
    {
        manager.SelectionUpdated -= Print;
    }

    private void Print(SelectionPath path)
    {
        string text = "";

        foreach (Cell cell in path.Cells)
        {
            text += $"[{cell.X},{cell.Y}] ";
        }

        Debug.Log(text);
    }
}