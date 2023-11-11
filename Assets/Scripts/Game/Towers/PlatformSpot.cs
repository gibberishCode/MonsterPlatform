using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

public class PlatformSpot : MonoBehaviour
{
    [SerializeField] private Renderer _reneder;
    [SerializeField] Vector3 _offset;

    public void Build() {
        var gameManager = ServiceLocator.Current.Get<GameManager>();
        var uiOpener = ServiceLocator.Current.Get<UIOptionsCreator>();
        var platformPiece = Instantiate(gameManager.TowersSettings.PlaftformPiecePrefab, transform.parent);
        platformPiece.transform.position = transform.position;
        platformPiece.transform.rotation = transform.rotation;
        Destroy(gameObject);
        uiOpener.Close();
    }

    internal void BuildWithTower(Tower towerPrefab)
    {
        var gameManager = ServiceLocator.Current.Get<GameManager>();
        var uiOpener = ServiceLocator.Current.Get<UIOptionsCreator>();
        var tower = Instantiate(towerPrefab, transform.parent);
        tower.transform.position = transform.position;
        tower.transform.rotation = transform.rotation;
        gameManager.Platform.RegisterPlatform(tower.PlatformPiece);
        Destroy(gameObject);
        uiOpener.Close();
    }
    
    

}
