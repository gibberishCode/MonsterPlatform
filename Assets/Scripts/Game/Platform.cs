using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using Core;
using MyUnityHelpers;
using SolidUtilities.UnityEngineInternals;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable]
public class PlatfromSettings
{
    public float MaxDistanceFromPlayer = 20;
    public float SqredMaxDistance => MaxDistanceFromPlayer * MaxDistanceFromPlayer;
}

public class Platform : MonoBehaviour, ITarget
{
    [SerializeField] PlatfromSettings _platformSettings;
    [SerializeField] Vector2Int PlatformGridDimensions;
    [SerializeField] Vector2 PlatformCellSize;
    [SerializeField] private GridDebugDrawer _gridDrawer;
    // [SerializeField] private TowerSpot _towerSpotPrefab;
    [SerializeField] private PlatformSpot _platformSpotPrefab;
    [SerializeField] private Vector3 _offsetBuildSpot;
    [SerializeField] private List<PlaftformPiece> _startingPlatforms;
    private TowerSpot _currentSpot;
    private PlatformGrid _grid;
    private PathMover _pathMover;
    private Player _player;
    private List<PlatformSpot> _platformSpots = new List<PlatformSpot>();
    private List<TowerSpot> _spots = new List<TowerSpot>();
    private Dictionary<Vector2Int, PlaftformPiece> _coordToPieces = new Dictionary<Vector2Int, PlaftformPiece>();


    public int PlatformPices => _coordToPieces.Count;
    public Vector3 Position => transform.position;

    private void Awake()
    {

        _player = ServiceLocator.Current.Get<GameManager>().Player;
        _pathMover = GetComponent<PathMover>();
        _pathMover.Target = _player;
        _pathMover.WaitingEvent += () => ToggleBuildMode(true);
        _pathMover.MovingEvent += () => ToggleBuildMode(false);
        Debug.Assert(_pathMover);
        _grid = new PlatformGrid(PlatformGridDimensions, PlatformCellSize, transform);
        _gridDrawer.Grid = _grid;

        // SpawnPlatformSpots(new HashSet<Vector2Int>() {new Vector2Int(1, 1)});
        foreach (var platform in _startingPlatforms)
        {
            RegisterPlatform(platform);
        }
        SpawnPlatformSpots();
        // _grid.GridMap((cell) =>
        // {
        //     if (cell.X == 1 && cell.Y == 1)
        //     {
        //         return;
        //     }
        //     var position = _grid.IndexToPosition(cell);
        //     int x = cell.X + - _grid.Width;
        //     int y = cell.Y + - _grid.Height;
        //     Quaternion rotation;
        //     if (x == -_grid.Width / 2) {
        //         rotation = Quaternion.Euler(0, 90, 0);
        //     } else if (x == _grid.Width / 2) {
        //         rotation = Quaternion.Euler(0, -90, 0);
        //     } else if (y == -_grid.Height / 2) {
        //         rotation = Quaternion.Euler(0, 0, 0);
        //     } else {
        //         rotation = Quaternion.Euler(0, 180, 0);
        //     }
        //     var spot = Instantiate(_platformSpotPrefab, transform);
        //     spot.transform.position = position + _offsetBuildSpot;
        //     spot.transform.rotation = rotation;

        //     // spot.PlayerIn += PlayerInSpot;
        //     // spot.PlayerOut += PlayerOutOfSpot;
        // });
    }

    public void ToggleBuildMode(bool state)
    {
        if (state)
        {
            SpawnPlatformSpots();
        }
        else
        {
            _platformSpots.DestroyAll();
            _platformSpots.Clear();
        }
    }

    public void SpawnPlatformSpots()
    {
        _platformSpots.DestroyAll();
        _platformSpots.Clear();
        var center = new Vector2Int(1, 1);
        var res = new HashSet<Vector2Int>();
        SpawnPlatformSpots(center, res, new List<Vector2Int>());
        foreach (var toSpawnCoord in res)
        {
            var pos = _grid.IndexToPosition(toSpawnCoord);
            var spot = Instantiate(_platformSpotPrefab, transform);
            spot.transform.position = pos + _offsetBuildSpot;
            // spot.transform.rotation = Quaternion.identity;
            _platformSpots.Add(spot);
        }
    }

    private void SpawnPlatformSpots(Vector2Int coord, HashSet<Vector2Int> outer, List<Vector2Int> visited)
    {
        if (visited.Contains(coord))
        {
            return;
        }
        visited.Add(coord);
        if (_coordToPieces.ContainsKey(coord))
        {
            SpawnPlatformSpots(coord + Vector2Int.down, outer, visited);
            SpawnPlatformSpots(coord + Vector2Int.right, outer, visited);
            SpawnPlatformSpots(coord + Vector2Int.left, outer, visited);
            SpawnPlatformSpots(coord + Vector2Int.up, outer, visited);
        }
        else
        {
            outer.Add(coord);
            return;
        }
    }

    public void SpawnPlatformSpots(HashSet<Vector2Int> platformCoords)
    {
        HashSet<Vector2Int> toSpawn = new HashSet<Vector2Int>();
        foreach (var platformCoord in platformCoords)
        {
            toSpawn.Add(platformCoord + Vector2Int.down);
            toSpawn.Add(platformCoord + Vector2Int.up);
            toSpawn.Add(platformCoord + Vector2Int.right);
            toSpawn.Add(platformCoord + Vector2Int.left);
        }
        foreach (var coord in toSpawn)
        {
            var pos = _grid.IndexToPosition(coord);
            var spot = Instantiate(_platformSpotPrefab, transform);
            spot.transform.position = pos + _offsetBuildSpot;
            spot.transform.rotation = Quaternion.identity;
        }
    }

    private void Start()
    {
        // Build();
    }

    private void Update()
    {
        var d = transform.position - _player.transform.position;
        if (d.sqrMagnitude > _platformSettings.SqredMaxDistance)
        {
            _pathMover.DesiredSpeed = 0;
        }
        else
        {
            _pathMover.DesiredSpeed = _pathMover.MaxSpeed;
        }
        foreach (var p in _coordToPieces.Values)
        {
            p.IsMoving = _pathMover.Speed > 0.1f;
        }
    }

    public Vector3 GetBuildSpot()
    {
        return new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2));
    }

    public void PlayerInSpot(TowerSpot spot)
    {
        if (_currentSpot)
        {
            _spots.Add(spot);
            return;
        }
        SetSpot(spot);
    }


    public void PlayerOutOfSpot(TowerSpot spot)
    {
        if (spot == _currentSpot)
        {
            _currentSpot.TryingToBuild(false);
            _currentSpot = null;
        }
        _spots.Remove(spot);
        if (_spots.Count > 0)
        {
            foreach (var next in _spots)
            {
                if (next)
                {
                    SetSpot(next);
                }
            }
        }

    }

    private void SetSpot(TowerSpot spot)
    {
        _currentSpot = spot;
        _currentSpot.TryingToBuild(true);
        _currentSpot.StartCoroutine(_currentSpot.Build());
    }

    public void RegisterPlatform(PlaftformPiece piece)
    {
        var coord = _grid.PositionToIndex(piece.transform.position);
        _coordToPieces.Add(coord, piece);
        SpawnPlatformSpots();
        Debug.Log($"Spawned p on {coord}");
    }

    public void UnregisterPlatform(PlaftformPiece piece)
    {
        var coord = _grid.PositionToIndex(piece.transform.position);
        _coordToPieces.Remove(coord);
        SpawnPlatformSpots();
    }

}
