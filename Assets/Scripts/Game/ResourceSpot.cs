using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpot : MonoBehaviour
{
    [SerializeField] ResourceInfo _resourceInfo;
    public ResourceInfo ResourceInfo => _resourceInfo;

    public int Consume(int toConsume)
    {
        var consumed = Math.Min(_resourceInfo.Amount, toConsume);
        _resourceInfo.Amount -= consumed;
        if (_resourceInfo.Amount <= 0)
        {
            Destroy(gameObject);
        }
        return consumed;
    }


}
