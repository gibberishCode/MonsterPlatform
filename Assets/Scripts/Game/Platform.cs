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
    [SerializeField] Vector2Int PlatformCellSize;
    [SerializeField] private GridDebugDrawer _gridDrawer;
    [SerializeField] private TowerSpot _towerSpotPrefab;
    [SerializeField] private Vector3 _offsetBuildSpot;
    private TowerSpot _currentSpot;
    private List<TowerSpot> _spots = new List<TowerSpot>();
    private PlatformGrid _grid;
    private TargetMover _mover;
    private Player _player;
    public Vector3 Position => transform.position;

    private void Awake()
    {

        _player = ServiceLocator.Current.Get<GameManager>().Player;
        _mover = GetComponent<TargetMover>();
        _mover.Target = _player;
        Debug.Assert(_mover);
        _grid = new PlatformGrid(PlatformGridDimensions, PlatformCellSize, this);
        _gridDrawer.Grid = _grid;
        _grid.GridMap((cell) =>
        {
            if (cell.X == 1 && cell.Y == 1)
            {
                return;
            }
            var position = _grid.IndexToPosition(cell);
            int x = cell.X + - _grid.Width;
            int y = cell.Y + - _grid.Height;
            Quaternion rotation;
            if (x == -_grid.Width / 2) {
                rotation = Quaternion.Euler(0, 90, 0);
            } else if (x == _grid.Width / 2) {
                rotation = Quaternion.Euler(0, -90, 0);
            } else if (y < 0) {
                rotation = Quaternion.Euler(0, 0, 0);
            } else {
                rotation = Quaternion.Euler(0, 180, 0);
            }
            var spot = Instantiate(_towerSpotPrefab, transform);
            spot.transform.position = position + _offsetBuildSpot;
            spot.transform.rotation = rotation;

            spot.PlayerIn += PlayerInSpot;
            spot.PlayerOut += PlayerOutOfSpot;
        });
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
            _mover.DesiredSpeed = 0;
        }
        else
        {
            _mover.DesiredSpeed = _mover.MaxSpeed;
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
        if (_spots.Count > 0) {
            foreach(var next in _spots) {
                if (next) {
                    SetSpot(next);
                }
            }
        }

    }

    private void SetSpot(TowerSpot spot) {
        _currentSpot = spot;
        _currentSpot.TryingToBuild(true);
        _currentSpot.StartCoroutine(_currentSpot.Build());
    }
}
