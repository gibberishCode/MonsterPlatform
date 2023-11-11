using System.ComponentModel.Design;
using Game.UI;
using MyUnityHelpers;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject GameOverScreen;
    public HeathDrawer PlayerHealth;
    
    private void Start() {
        var player = ServiceLocator.Current.Get<GameManager>().Player;
        PlayerHealth.SetTarget(player.GetComponent<Damageable>());
    }

    public void SetGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }
}