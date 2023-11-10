using MyUnityHelpers;
using UnityEngine;



public class Tower : MonoBehaviour
{
    [SerializeField] PlaftformPiece _platformPiece;
    public PlaftformPiece PlatformPiece => _platformPiece;
    
    
    public void Unregister() {
        var gameManager = ServiceLocator.Current.Get<GameManager>();
        gameManager.Platform.UnregisterPlatform(_platformPiece);
    }
}