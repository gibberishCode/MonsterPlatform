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

    private void Start()
    {
        _spawnTimer = new FrequencyExecutor(Frequency, this,
        Spawn);
    }

    private void Spawn()
    {
        var pos = Random.insideUnitCircle.Vector2FlatToX0Z() * Range;
        var enemy = Instantiate(_enemyPrefabs.GetRandom());
        enemy.transform.position = pos;
        //TODO fix
        enemy.GetComponent<TargetMover>().Target = FindAnyObjectByType<Player>();
    }

}
