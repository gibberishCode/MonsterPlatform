
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
        if (IsTarget(gameObject)  && _executor == null)
        {
            _target = gameObject;
            _executor = new FrequencyExecutor(_frequency, this, OnExecute);
            InRangeEvent?.Invoke(gameObject);
        }
    }

    private void OnDisable() {
        if (_executor != null) {
            _executor.Stop();
        }
    }

    private void OnEnable() {
        if (_executor != null) {
            _executor.Start();
        }
    }

    private void OnExecute()
    {
        if (_executor == null ) {
            Debug.LogWarning("Executor is null ", this);
            return;
        }
        if (!_executor.IsRunning) {
            Debug.LogWarning("Executor is stopped ", this);
            return;
        }
        if ( _target)
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
        if (_executor!= null&& _target == gameObject)
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