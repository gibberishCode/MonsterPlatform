using MyUnityHelpers;
using UnityEngine;



public class Tower : MonoBehaviour
{
    [SerializeField] PlaftformPiece _platformPiece;
    public PlaftformPiece PlatformPiece => _platformPiece;
    
    
    private void Start() {
        _platformPiece.OnDiedEvent += Die;
    }
    
    public void Unregister() {
        var gameManager = ServiceLocator.Current.Get<GameManager>();
        gameManager.Platform.UnregisterPlatform(_platformPiece);
    }
    
    private void Die() {
        Unregister();
        Destroy(gameObject);

    }
}