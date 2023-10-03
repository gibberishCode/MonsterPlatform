using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using MyUnityHelpers;
using SolidUtilities.UnityEngineInternals;
using UnityEngine;
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
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private GridDebugDrawer _gridDrawer;
    [SerializeField] private TowerSpot _towerSpotPrefab;
    private TowerSpot _currentSpot;
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
