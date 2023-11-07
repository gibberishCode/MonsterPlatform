using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

public class PlatformSpot : MonoBehaviour
{
    [SerializeField] private Renderer _reneder;

    public void Build() {
        var gameManager = ServiceLocator.Current.Get<GameManager>();
        var uiOpener = ServiceLocator.Current.Get<UIOptionsCreator>();
        var platformPiece = Instantiate(gameManager.TowersSettings.PlaftformPiecePrefab, transform.parent);
        platformPiece.transform.position = transform.position;
        platformPiece.transform.rotation = transform.rotation;
        Destroy(gameObject);
        uiOpener.Close();
    }

}
