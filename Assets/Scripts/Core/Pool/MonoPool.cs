using UnityEngine;

namespace Core {
    [CreateAssetMenu(fileName = "MonoPool", menuName = "Core/MonoPool")]
    public class MonoPool : ObjectPool<MonoBehaviour> {
    }
}