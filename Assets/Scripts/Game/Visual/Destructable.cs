using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

public class Destructable : MonoBehaviour {

    [SerializeField] private GameObjectPool _particlePool;
    public float DelayDestory = 0.1f;

    private void Start() {
        _particlePool.Init();
        // Expload();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.isTrigger) {
            return;
        }
        Expload();
    }

    private void Expload() {
        StartCoroutine(DelayDestroy(DelayDestory));
    }

    private IEnumerator DelayDestroy(float delay) {
        yield return new WaitForSeconds(delay);
        var prefab = _particlePool.Get();
        var particle = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        var pos = transform.position;
        pos.y = 0;
        particle.transform.position = pos;
        var box = GetComponent<BoxCollider>();
        if (box.size.magnitude > 1) {
            particle.transform.localScale = Vector3.one * box.size.magnitude / 8;
        }
        Destroy(gameObject);
    }
}
