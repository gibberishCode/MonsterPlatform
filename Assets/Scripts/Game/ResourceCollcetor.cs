using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;
using UnityEngine.Events;

public class ResourceCollcetor : InTriggerActionExecutor
{
    public UnityEvent<ResourceSpot> ResourceCollectedEvent;
    public int CollectedAtOnce = 1;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = ServiceLocator.Current.Get<GameManager>();
    }

    protected override bool IsTarget(GameObject obj)
    {
        return obj.GetComponent<ResourceSpot>();
    }

    public void Collect()
    {
        Debug.Log("Collecting");
        var spot = _target.GetComponent<ResourceSpot>();
        Debug.Assert(spot);
        var collected = spot.Consume(CollectedAtOnce);
        _gameManager.GameData.Resources[(int)(spot.ResourceInfo.Type)].Amount += collected;
        ResourceCollectedEvent?.Invoke(spot);
    }
}
