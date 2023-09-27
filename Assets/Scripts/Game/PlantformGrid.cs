using Core;
using UnityEngine;

public class PlatformGrid : WorldGrid
{
    public PlatformGrid(Vector2Int dimensions, Vector2 cellSize, ITarget origin) : base(dimensions.x, dimensions.y, cellSize, origin)
    {
        Cells = new PlacementArea[dimensions.x, dimensions.x];
        for (int y = 0; y < dimensions.y; y++)
        {
            for (int x = 0; x < dimensions.x; x++)
            {
                Cells[x, y] = new PlacementArea(x, y);
            }
        }
    }
}