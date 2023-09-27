using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float Damage = 1;
    [SerializeField] LineRenderer _lineRenderer;
    private Enemy _target;
    private List<Enemy> _queue = new List<Enemy>();

    private void Update()
    {
        if (_target)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _target.transform.position);
            var damageable = _target.GetComponent<Damageable>();
            damageable.DealDamage(Damage * Time.deltaTime);
        }
        else
        {
            if (_queue.Count == 0)
            {
                _lineRenderer.enabled = false;
            }
            else
            {

                _target = _queue[0];
                _queue.RemoveAt(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            if (_target)
            {
                _queue.Add(enemy);
            }
            else
            {
                _target = enemy;
                _lineRenderer.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        var enemy = other.GetComponent<Enemy>();
        _queue.Remove(enemy);
    }

}
