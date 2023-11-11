using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackTower : Tower
{
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] Property _damage;
    
    public void SpawnProjectile(Damageable damageable) {
        var projectile = Instantiate(_projectilePrefab);
        projectile.transform.position = _spawnPoint.position;
        projectile.Init(damageable, _damage.MaxValue);
    }
    
    public void AddToMultiplier(float m) {
        _damage.Multiplier += m;
    }
    // public float Damage = 1;
    // [SerializeField] LineRenderer _lineRenderer;
    // private Enemy _target;
    // private List<Enemy> _queue = new List<Enemy>();

    // private void Update()
    // {
    //     if (_target)
    //     {
    //         _lineRenderer.SetPosition(0, transform.position);
    //         _lineRenderer.SetPosition(1, _target.transform.position);
    //         var damageable = _target.GetComponent<Damageable>();
    //         damageable.DealDamage(Damage * Time.deltaTime);
    //     }
    //     else
    //     {
    //         if (_queue.Count == 0)
    //         {
    //             _lineRenderer.enabled = false;
    //         }
    //         else
    //         {

    //             _target = _queue[0];
    //             _queue.RemoveAt(0);
    //         }
    //     }
    // }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.isTrigger)
    //     {
    //         return;
    //     }
    //     var enemy = other.GetComponent<Enemy>();
    //     if (enemy)
    //     {
    //         if (_target)
    //         {
    //             _queue.Add(enemy);
    //         }
    //         else
    //         {
    //             _target = enemy;
    //             _lineRenderer.enabled = true;
    //         }
    //     }
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.isTrigger)
    //     {
    //         return;
    //     }
    //     var enemy = other.GetComponent<Enemy>();
    //     _queue.Remove(enemy);
    // }

}
