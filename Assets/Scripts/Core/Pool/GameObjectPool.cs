using UnityEngine;

namespace Core {
    [CreateAssetMenu(fileName = "GameObjectPool", menuName = "Core/GameObjectPool")]
    public class GameObjectPool : ObjectPool<GameObject> {
    }
}