using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "MonsterPlatform/GameSettings", order = 0)]
public class GameSettings : ScriptableObject {
    public float PlatformHealthRecoveringSpeed;
    public float TowerCostRate = 1.12f;
    [SerializeField] private int _maxEnemies = 5;
    public int MaxEnemies => _maxEnemies;
    
}