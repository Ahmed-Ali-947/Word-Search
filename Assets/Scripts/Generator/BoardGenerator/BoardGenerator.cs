using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private DirectionProvider directionProvider;

    private readonly PlacementValidator validator = new();
    private readonly WordPlacer placer = new();
    private readonly LetterFiller filler = new();

    private const int MaxPlacementAttempts = 100;

    public BoardGenerationResult Generate(
        List<WordData> words,
        LevelSettings settings, System.Random random)
    {
        BoardGenerationResult result = new();

        char[,] board = new char[
            settings.Width,
            settings.Height];

        List<Vector2Int> directions =
            directionProvider.GetDirections(settings);

        foreach (WordData word in words)
        {
            bool placed = false;

            for (int attempt = 0;
                 attempt < MaxPlacementAttempts;
                 attempt++)
            {
                Vector2Int direction =
                    directions[
                        random.Next(directions.Count)];

                Vector2Int start =
                    new(
                        random.Next(settings.Width),
                        random.Next(settings.Height));

                if (!validator.CanPlace(
                        board,
                        word.Text,
                        start,
                        direction))
                {
                    continue;
                }

                placer.Place(
                    board,
                    word.Text,
                    start,
                    direction);

                result.Placements.Add(
                    new WordPlacement
                    {
                        Word = word,
                        Start = start,
                        Direction = direction
                    });

                placed = true;
                break;
            }

            if (!placed)
            {
                result.Success = false;
                return result;
            }
        }

        filler.Fill(board, random);

        result.Success = true;
        result.Letters = board;

        return result;
    }
}