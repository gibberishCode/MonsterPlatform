using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour {
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private Player _player;
    private Mover _mover;

    private void Awake() {
        _mover = GetComponent<Mover>();
        _mover.Target = _player;
    }

    private void Start() {
        Build();
    }

    public void Build() {
        var tower = Instantiate(_towerPrefab);
        tower.transform.position = GetBuildSpot();
        tower.transform.parent = transform;
    }

    public Vector3 GetBuildSpot() {
        return new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2));
    }
}
