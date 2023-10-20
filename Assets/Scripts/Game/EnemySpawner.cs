using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public float Range = 40;
    public float Frequency = 1.0f / 5.0f;
    [SerializeField] private List<Enemy> _enemyPrefabs;
    FrequencyExecutor _spawnTimer;
    GameManager _gameManager;

    private void Start()
    {
        _spawnTimer = new FrequencyExecutor(Frequency, this, Spawn);
        _gameManager = ServiceLocator.Current.Get<GameManager>();
    }

    private void Spawn()
    {
        var pos =  _gameManager.Player.transform.position + Random.insideUnitCircle.Vector2FlatToX0Z() * Range;
        pos.y = 0;
        var enemy = Instantiate(_enemyPrefabs.GetRandom());
        enemy.transform.position = pos;
        //TODO fix
        enemy.GetComponent<TargetMover>().Target = FindAnyObjectByType<Player>();
    }


}
