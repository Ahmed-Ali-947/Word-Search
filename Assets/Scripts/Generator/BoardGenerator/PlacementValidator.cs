using UnityEngine;

public class PlacementValidator
{
    public bool CanPlace(
        char[,] board,
        string word,
        Vector2Int start,
        Vector2Int direction)
    {
        int width = board.GetLength(0);
        int height = board.GetLength(1);

        for (int i = 0; i < word.Length; i++)
        {
            int x = start.x + direction.x * i;
            int y = start.y + direction.y * i;

            if (x < 0 || x >= width)
                return false;

            if (y < 0 || y >= height)
                return false;

            char existing = board[x, y];

            if (existing != '\0' &&
                existing != word[i])
            {
                return false;
            }
        }

        return true;
    }
}