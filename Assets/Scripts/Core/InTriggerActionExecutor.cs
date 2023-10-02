
using System;
using UnityEngine;
using UnityEngine.Events;
public class InTriggerActionExecutor : MonoBehaviour
{
    [SerializeField] protected float _frequency;
    public UnityEvent ExecuteEvent;
    public UnityEvent<GameObject> InRangeEvent;
    public UnityEvent<GameObject> OutOfRangeEvent;

    private FrequencyExecutor _executor;
    protected GameObject _target;

    protected virtual bool IsTarget(GameObject obj)
    {
        return true;
    }

    private void OnInRange(GameObject gameObject)
    {
        if (IsTarget(gameObject))
        {
            _target = gameObject;
            _executor = new FrequencyExecutor(_frequency, this, OnExecute);
            InRangeEvent?.Invoke(gameObject);
        }
    }

    private void OnExecute()
    {
        if (_target)
        {
            ExecuteEvent?.Invoke();
        }
        else
        {
            _executor.Stop();
            _executor = null;
        }
    }

    private void OnOutRange(GameObject gameObject)
    {
        if (_target == gameObject)
        {
            _target = null;
            _executor.Stop();
            _executor = null;
            OutOfRangeEvent?.Invoke(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnInRange(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        OnOutRange(other.gameObject);
    }
}