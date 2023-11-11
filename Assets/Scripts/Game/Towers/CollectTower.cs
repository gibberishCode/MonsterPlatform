using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

public class CollectTower : Tower
{

    private ResourceManager _resourceManager;
    private bool _isProcessing;

    private void Awake() {
        _resourceManager = ServiceLocator.Current.Get<ResourceManager>();
    }

    public void OnResource(GameObject collider) {
        var resource = collider.GetComponent<ResourceSpot>();
        var consumed = resource.Consume(100);
        _resourceManager.AddResources(consumed, resource.ResourceInfo.Type);
    }
}
