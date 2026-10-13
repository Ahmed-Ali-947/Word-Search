public class CompletedPath
{
    public string Word { get; }

    public SelectionPath Path { get; }

    public PathView View { get; }

    public CompletedPath(string word, SelectionPath path, PathView view)
    {
        Word = word;
        Path = path;
        View = view;
    }
}