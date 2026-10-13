using UnityEngine;

public class LetterFiller
{
    // Letter pool such that some letters are more common than others
    private const string LetterPool =
        "EEEEEEEEEEEEAAAAAAAARRRRRRRRIIIIIIIIOOOOOOOOTTTTTTTTNNNNNNNNSSSSSSSSLLLLLLCCCCCCUUUUUUDDDDDDPPPPPPMMMMMMHHHHHHGGGGGGFFFFBBBBYYYYWWWWKKKVVVXXZZJJQQ";

    public void Fill(char[,] board, System.Random random)
    {
        int width = board.GetLength(0);
        int height = board.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (board[x, y] == '\0')
                {
                    board[x, y] =
                        LetterPool[
                            random.Next(LetterPool.Length)];
                }
            }
        }
    }
}