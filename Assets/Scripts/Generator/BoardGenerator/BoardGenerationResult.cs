using System.Collections.Generic;

public class BoardGenerationResult
{
    public bool Success;

    public char[,] Letters;

    public List<WordPlacement> Placements = new();
}