using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Core
{
    public interface ICell
    {
        int X { get; }
        int Y { get; }
    }

    public class Grid
    {
        public ICell[,] Cells { get; protected set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Grid(int width, int height)
        {
            Cells = new ICell[width, height];
            Width = width;
            Height = height;
        }

        public ICell GetCell(int x, int y)
        {
            return Cells[x, y];
        }

        public void SetCell(int x, int y, ICell cell)
        {
            Cells[x, y] = cell;
        }
    }

    public class WorldGrid : Grid
    {
        public Vector2 CellSize { get; set; }
        public Transform Origin { get; set; }

        public WorldGrid(int width, int height, Vector2 cellSize, Transform origin) : base(width, height)
        {
            CellSize = cellSize;
            Origin = origin;
        }

        public void SetGrid(ICell[,] cells)
        {
            Cells = cells;
        }

        public Vector3 IndexToPosition(ICell cell)
        {
            return IndexToPosition(new Vector2Int(cell.X, cell.Y));
        }

        public Vector3 IndexToPosition(Vector2Int index)
        {
            var worldPos = index * CellSize
                           - new Vector2(CellSize.x * Width, CellSize.y * Height) / 2;
            worldPos += CellSize / 2;
            var vector =  new Vector3(worldPos.x, 0, worldPos.y);
            vector = Origin.TransformVector(vector);
            return Origin.position + vector;
        }

        public Vector2Int PositionToIndex(Vector3 position)
        {
            // position -= Origin.Position;
            position = Origin.InverseTransformPoint(position);
            // position += new Vector3(CellSize.x, 0, CellSize.y) / 2;
            var res = new Vector2Int(Width / 2, Height / 2) +
                      new Vector2Int((int)(position.x / (CellSize.x)),
                          (int)(position.z / (CellSize.y)));
            // return new Vector2Int((int)(position.x / (CellDimensions.x * 2)),
            // (int)(position.z / (CellDimensions.y * 2)));
            return res;
        }

    }

    public static class GridExtensions
    {
        public static void GridMap(this Grid grid, Action<ICell> func)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Height; x++)
                {
                    func(grid.Cells[x, y]);
                }
            }
        }
    }
}