using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour, ITarget
{
    [SerializeField] Vector2Int PlatformGridDimensions;
    [SerializeField] Vector2Int PlatformCellSize;
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private GridDebugDrawer _gridDrawer;
    [SerializeField] private TowerSpot _towerSpotPrefab;
    private TowerSpot _currentSpot;
    private PlatformGrid _grid;
    private TargetMover _mover;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _mover = GetComponent<TargetMover>();
        _mover.Target = _player;
        _grid = new PlatformGrid(PlatformGridDimensions, PlatformCellSize, this);
        _gridDrawer.Grid = _grid;
        _grid.GridMap((cell) =>
        {
            if (cell.X == 1 && cell.Y == 1)
            {
                return;
            }
            var position = _grid.IndexToPosition(cell);
            var spot = Instantiate(_towerSpotPrefab, transform);
            spot.transform.position = position + Vector3.up * 0.5f;
            spot.PlayerIn += PlayerInSpot;
            spot.PlayerOut += PlayerOutOfSpot;
        });
    }

    private void Start()
    {
        // Build();
    }

    public void Build()
    {
        var tower = Instantiate(_towerPrefab);
        tower.transform.position = GetBuildSpot();
        tower.transform.parent = transform;
    }

    public Vector3 GetBuildSpot()
    {
        return new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2));
    }

    public void PlayerInSpot(TowerSpot spot)
    {
        if (_currentSpot)
        {
            return;
        }
        _currentSpot = spot;
        _currentSpot.TryingToBuild(true);
        _currentSpot.StartCoroutine(_currentSpot.Build());
    }

    public void PlayerOutOfSpot(TowerSpot spot)
    {
        if (spot == _currentSpot)
        {

            _currentSpot.TryingToBuild(false);
            _currentSpot = null;
        }
    }
}
