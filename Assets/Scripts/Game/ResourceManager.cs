using System;
using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

public class ResourceManager : MonoBehaviour, IGameService
{

    private GameManager _gameManager;

    public List<ResourceInfo> Resources => _gameManager.GameData.Resources;

    private void Awake()
    {
        _gameManager = ServiceLocator.Current.Get<GameManager>();
    }

    private void OnGUI()
    {
        GUIStyle headStyle = new GUIStyle();
        headStyle.fontSize = 50;
        headStyle.fontStyle = FontStyle.Bold;
        for (int i = 0; i < _gameManager.GameData.Resources.Count; i++)
        {
            ResourceInfo r = _gameManager.GameData.Resources[i];
            var rect = new Rect(250 * i, 0, 250, 300);
            GUI.Label(rect, $"{r.Type}: {(int)r.Amount}", headStyle);
        }
    }
    public void AddResources(int amount, ResourceTypes type) {
        foreach (var resource in Resources) {
            if (resource.Type == type ){
                resource.Amount += amount;
                return;
            }
        }
        Resources.Add(new ResourceInfo(amount, type));
    }
    
    ResourceInfo MatchResourceInfo(ResourceInfo resourceInfo) {
        foreach(var resoruce in Resources) {
            if (resoruce.Type == resourceInfo.Type) {
                return resoruce;
            }
        }
        return null;

    }
    
    public bool CanSpend(List<ResourceInfo> resources) {
        foreach(var spend in resources) {
            var have = MatchResourceInfo(spend);
            if (have == null || have.Amount < spend.Amount) {
                return false;
            }
        }
        return true;
    }

    internal void Spend(List<ResourceInfo> toSpend)
    {
        Debug.Assert(CanSpend(toSpend));
        foreach (var r in toSpend) {
            var have = MatchResourceInfo(r);
            have.Amount -= r.Amount;
        }
        
    }
}
