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
            GUI.Label(rect, $"{r.Type}: {r.Amount}", headStyle);
        }
    }
    public void AddResources(int amount, ResourceTypes type) {
        foreach (var resource in Resources) {
            if (resource.Type == type ){
                resource.Amount += amount;
                return;
            }
        }
        Resources.Add(new ResourceInfo(){Type = type, Amount = amount});
    }


}
