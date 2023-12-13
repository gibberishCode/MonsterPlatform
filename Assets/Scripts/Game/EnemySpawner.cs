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
    private int _currentDifficulty = 1;

    private void Start() {
        _gameManager = ServiceLocator.Current.Get<GameManager>();
        _player = _gameManager.Player;
        Enemy.DiedEvent += () => {
            _spawned--;
            _spawnTimer.Start();
        };
        StartCoroutine(StartEnemySpawing());
    }
    
    private IEnumerator IncreaseDifficulty() {
        while (true) {
            yield return new WaitForSeconds(_gameSettings.DifficultyIncreaseTimer);
            _currentDifficulty++;
        }
    }
    
    private void Update() {
        transform.position = _player.transform.position;
    }
    
    private IEnumerator StartEnemySpawing() {
        yield return new WaitForSeconds(_gameSettings.InitialEnemySpawnDelay);
        _spawnTimer = new FrequencyExecutor(Frequency, this, Spawn);
    }

    private void Spawn() {
        var vector = Quaternion.Euler(0, Random.Range(0, 360), 0) * Vector3.forward;
        var pos = _gameManager.Player.transform.position + vector * Range;
        pos.y = 0;
        var enemy = Instantiate(_enemyPrefabs.GetRandom());
        enemy.transform.position = pos;
        //TODO fix
        enemy.GetComponent<TargetMover>().Target = _player;
        enemy.Init(_currentDifficulty);
        _spawned++;
        if (_spawned == _gameSettings.MaxEnemies) {
            _spawnTimer.Stop();
        }
    }
}
