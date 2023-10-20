using System;
using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class GameData
{
    public List<ResourceInfo> Resources = new List<ResourceInfo>();
}

public class GameManager : MonoBehaviour, IGameService
{

    [SerializeField] ResourceManager _resourceManager;
    [SerializeField] private Player _player;
    [SerializeField] private Joystick _joystick;
    [SerializeField] GameData _gameData;
    [SerializeField] UI _ui;
    public GameData GameData => _gameData;
    public Player Player => _player;


    private void Awake()
    {
        ServiceLocator.Initiailze();
        ServiceLocator.Current.Register(this);
        ServiceLocator.Current.Register(_resourceManager);
    }

    private void Start()
    {


    }

    private void Update()
    {
        // if (_joystick.Horizontal ==  0 && _joystick.)
        // {

        // }
        _player.SetDirection(new Vector3(_joystick.Horizontal, 0, _joystick.Vertical));
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
        _ui.SetGameOverScreen();
    }

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
