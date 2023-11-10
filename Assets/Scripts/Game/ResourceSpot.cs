using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpot : MonoBehaviour
{
    public event Action Destroyed;
    [SerializeField] ResourceInfo _resourceInfo;
    public ResourceInfo ResourceInfo => _resourceInfo;

    public int Consume(int toConsume)
    {
        var consumed = Math.Min(_resourceInfo.Amount, toConsume);
        _resourceInfo.Amount -= consumed;
        if (_resourceInfo.Amount <= 0)
        {
            Destroyed?.Invoke();
            Destroy(gameObject);
        }
        return (int)consumed;
    }


}
