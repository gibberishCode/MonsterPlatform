using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

public class RandomResourceSpawner : MonoBehaviour
{
    [SerializeField] List<ResourceSpot> SpotPrefabs = new List<ResourceSpot>();
    [SerializeField] float SpawnRange = 100;
    [SerializeField] float SpawnScatter = 10;
    [SerializeField] int _spawnAmount = 10;
    private const int SpwanAttempts = 10;
    private List<ResourceSpot> _spawned = new List<ResourceSpot>();

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            for (int attempt = 0; attempt < SpwanAttempts; attempt++)
            {
                var randomPos = Random.insideUnitCircle.Vector2FlatToX0Z() * SpawnRange;
                bool good = true;
                foreach (var spawned in _spawned)
                {
                    var d = Vector3.Distance(spawned.transform.position, randomPos);
                    if (d < SpawnScatter)
                    {
                        good = false;
                        break;
                    }
                }
                if (good)
                {
                    var resource = Instantiate(SpotPrefabs.GetRandom(), transform);
                    resource.transform.position = randomPos;
                    _spawned.Add(resource);
                    break;
                }
            }
        }
    }
}
