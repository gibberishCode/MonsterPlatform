using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyExecutor : IDisposable
{
    public event Action OnFire;
    public float Frequency;
    private MonoBehaviour _actor;
    private Coroutine _courutine;
    private bool _isRunning;
    public bool IsRunning => _isRunning;

    public FrequencyExecutor(float frequency, MonoBehaviour actor, Action callback = null, bool autoRun = true)
    {
        Frequency = frequency;
        _actor = actor;
        if (callback != null)
        {
            OnFire += callback;
        }
        if (autoRun)
        {
            Start();
        }
    }

    public void Start()
    {
        Debug.Assert(_actor);
        _isRunning = true;
        _courutine = _actor.StartCoroutine(Run());
    }

    public void Stop()
    {
        _actor?.StopCoroutine(_courutine);
        _isRunning = false;
    }

    private IEnumerator Run()
    {
        while (_isRunning)
        {
            yield return new WaitForSeconds(1.0f / Frequency);
            OnFire?.Invoke();
        }

    }

    public void Dispose()
    {
        Stop();
    }
}