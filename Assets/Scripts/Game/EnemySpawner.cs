using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    public float Range = 40;
    public float Frequency = 1.0f / 5.0f;
    [SerializeField] private Enemy _enemyPrefab;
    FrequencyExecutor _spawnTimer;

    private void Start()
    {
        _spawnTimer = new FrequencyExecutor(Frequency, this,
        Spawn);
    }

    private void Spawn()
    {
        var pos = Random.insideUnitCircle.Vector2FlatToX0Z() * Range;
        var enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = pos;
        enemy.GetComponent<Mover>().Target = FindAnyObjectByType<Player>();
    }

}
