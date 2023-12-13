using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "MonsterPlatform/GameSettings", order = 0)]
public class GameSettings : ScriptableObject {
    public float PlatformHealthRecoveringSpeed;
    public float TowerCostRate = 1.12f;
    public float InitialEnemySpawnDelay = 10.0f;
    public float EnemyHealthIncreaseRate = 1.5f;
    public float IncreaseEnemyAttackRate = 1.5f;
    public float DifficultyIncreaseTimer = 20;
    [SerializeField] private int _maxEnemies = 5;
    public int MaxEnemies => _maxEnemies;
    
}