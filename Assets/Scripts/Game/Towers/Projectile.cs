using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float _speed;
    private Damageable _target;
    private float _damage;
        

    public void Init(Damageable target, float damage) {
        _target = target;
        _damage = damage;
    }
    

    private void Update() {
        if (!(_target && _target.gameObject)) {
            Destroy(gameObject);
            return;
        }
        var dir = _target.transform.position - transform.position;
        dir.Normalize();
        transform.position += _speed * Time.deltaTime * dir;
    }
    

    private void OnTriggerEnter(Collider other) {
        var damegeable = other.GetComponent<Damageable>();
        if (damegeable) {
            damegeable.DealDamage(_damage);
            Destroy(gameObject);
        }

    }
}