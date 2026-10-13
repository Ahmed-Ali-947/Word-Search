using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private readonly List<CompletedPath> completedPaths = new();

    public IReadOnlyList<CompletedPath> CompletedPaths => completedPaths;
    public int FoundWordCount => completedPaths.Count;

    public bool HasFoundWord(string word)
    {
        foreach (CompletedPath completedPath in completedPaths)
        {
            if (completedPath.Word == word)
                return true;
        }

        return false;
    }

    public void Register(string word, SelectionPath path, PathView view)
    {
        completedPaths.Add(
            new CompletedPath(
                word,
                path,
                view));
    }

    public void Clear()
    {
        foreach (CompletedPath completedPath in completedPaths)
        {
            if (completedPath.View != null)
                Destroy(completedPath.View.gameObject);
        }

        completedPaths.Clear();
    }

}