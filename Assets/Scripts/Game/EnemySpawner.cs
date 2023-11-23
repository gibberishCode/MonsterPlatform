using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private GameSettings _gameSettings;
    public float Range = 40;
    public float Frequency = 1.0f / 5.0f;
    private FrequencyExecutor _spawnTimer;
    private GameManager _gameManager;
    private int _spawned;
    private Player _player;

    private void Start() {
        _spawnTimer = new FrequencyExecutor(Frequency, this, Spawn);
        _gameManager = ServiceLocator.Current.Get<GameManager>();
        _player = _gameManager.Player;
        Enemy.DiedEvent += () => {
            _spawned--;
            _spawnTimer.Start();
        };
    }
    
    private void Update() {
        transform.position = _player.transform.position;
    }

    private void Spawn() {
        var vector = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward;
        var pos = _gameManager.Player.transform.position + vector * Range;
        pos.y = 0;
        var enemy = Instantiate(_enemyPrefabs.GetRandom());
        enemy.transform.position = pos;
        //TODO fix
        enemy.GetComponent<TargetMover>().Target = _player;
        _spawned++;
        if (_spawned == _gameSettings.MaxEnemies) {
            _spawnTimer.Stop();
        }
    }


}
