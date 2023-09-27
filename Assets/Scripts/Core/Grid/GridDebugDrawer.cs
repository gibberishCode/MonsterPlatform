using System.Security;
using System.Security.Cryptography;
using MyUnityHelpers;
using UnityEngine;

namespace Core
{
    public class GridDebugDrawer : MonoBehaviour
    {
        public enum DrawType
        {
            Wired,
            Solid
        }
        public WorldGrid Grid { get; set; }
        public Color Color = new Color(1, 0, 0, 0.5f);
        public float Scale = 1;
        public DrawType Type;

        private void OnDrawGizmos()
        {
            if (Grid == null)
            {
                return;
            }

            Gizmos.color = Color;


            Grid.GridMap(cell =>
            {
                var pos = Grid.IndexToPosition(cell);
                var size = (Vector3)Grid.CellSize;
                size.z = size.y;
                if (Type == DrawType.Wired)
                {
                    Gizmos.DrawWireCube(pos, size * Scale);
                }
                else
                {
                    Gizmos.DrawCube(pos, size * Scale);
                }
            });
        }
    }
}