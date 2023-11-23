using UnityEngine;
namespace Core
{
    using System.Collections.Generic;
    using UnityEngine;

    public class ObjectPool<T> : ScriptableObject where T : Object
    {
        [SerializeField] private T[] _prefabs;
        [SerializeField] private int _preCreate = 5;
        private List<T> _spawned;
        private Transform _parent;

        public void Init(Transform parent = null)
        {
            _spawned = new List<T>();
            _parent = parent;
            for (var i = 0; i < _preCreate; i++)
            {
                var random = Random.Range(0, _prefabs.Length);
                var obj = Create(random);
                if (obj is GameObject go)
                {
                    go.SetActive(false);
                }
                else if (obj is Component component)
                {
                    component.gameObject.SetActive(false);
                }
            }
        }

        public T Get()
        {
            foreach (var spawned in _spawned)
            {
                if (spawned is GameObject go && !go.activeSelf)
                {
                    go.SetActive(true);
                    return spawned;
                }
                else if (spawned is Component component && !component.gameObject.activeSelf)
                {
                    component.gameObject.SetActive(true);
                    return spawned;
                }

            }
            var random = Random.Range(0, _prefabs.Length);
            return Create(random);
        }

        public void Release(T obj)
        {
            if (obj is GameObject go)
            {
                go.SetActive(false);
            }
            else if (obj is Component component)
            {
                component.gameObject.SetActive(false);
            }
        }

        private T Create(int index)
        {
            var spawned = Instantiate(_prefabs[index], _parent);
            _spawned.Add(spawned);
            return spawned;
        }

    }
}