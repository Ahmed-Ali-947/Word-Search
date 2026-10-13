using UnityEngine;

public class WordPlacer
{
    public void Place(
        char[,] board,
        string word,
        Vector2Int start,
        Vector2Int direction)
    {
        for (int i = 0; i < word.Length; i++)
        {
            int x = start.x + direction.x * i;
            int y = start.y + direction.y * i;

            board[x, y] = word[i];
        }
    }
}