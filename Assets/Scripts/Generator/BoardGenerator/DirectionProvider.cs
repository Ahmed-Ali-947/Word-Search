using System.Collections.Generic;
using UnityEngine;

public class DirectionProvider : MonoBehaviour
{
    private static readonly Vector2Int[] CardinalDirections =
    {
        Vector2Int.right,
        Vector2Int.left,
        Vector2Int.up,
        Vector2Int.down
    };

    private static readonly Vector2Int[] DiagonalDirections =
    {
        new(1,1),
        new(1,-1),
        new(-1,1),
        new(-1,-1)
    };

    public List<Vector2Int> GetDirections(LevelSettings settings)
    {
        List<Vector2Int> result = new();

        result.AddRange(CardinalDirections);

        if (settings.AllowDiagonal)
            result.AddRange(DiagonalDirections);

        if (!settings.AllowBackwards)
        {
            result.Remove(Vector2Int.left);
            result.Remove(Vector2Int.down);
            result.Remove(new Vector2Int(-1, 1));
            result.Remove(new Vector2Int(-1, -1));
        }

        return result;
    }
}