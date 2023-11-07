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
    [SerializeField] UIOptionsCreator _uiOptionsCreator;
    [SerializeField] private Player _player;
    [SerializeField] private Joystick _joystick;
    [SerializeField] GameData _gameData;
    [SerializeField] UI _ui;
    [SerializeField] TowersSettings _towerSettings;
    [SerializeField] Camera _camera;
    [SerializeField] LayerMask _interactable;
    public GameData GameData => _gameData;
    public Player Player => _player;
    public TowersSettings TowersSettings => _towerSettings;


    private void Awake()
    {
        ServiceLocator.Initiailze();
        ServiceLocator.Current.Register(this);
        ServiceLocator.Current.Register(_resourceManager);
        ServiceLocator.Current.Register(_uiOptionsCreator);
    }

    private void Start()
    {


    }

    private void Update()
    {
        // if (_joystick.Horizontal ==  0 && _joystick.)
        // {

        // }
        if (Input.GetMouseButton(0))
        {
            ProcessClick();
        }
        _player.SetDirection(new Vector3(_joystick.Horizontal, 0, _joystick.Vertical));
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            ServiceLocator.Current.Get<UIOptionsCreator>().Close();
        }
    }

    private void ProcessClick()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var hits = Physics.RaycastAll(ray, 1000);
        foreach (var hit in hits)
        {
            var platformPiece = hit.collider.GetComponent<PlaftformPiece>();
            if (platformPiece)
            {
                _uiOptionsCreator.Open(_towerSettings.TowerBuildOptions, platformPiece.transform);
            }
            else
            {

                var platformSpot = hit.collider.GetComponent<PlatformSpot>();
                if (platformSpot)
                {
                    _uiOptionsCreator.Open(_towerSettings.PlatformBuildOptions, platformSpot.transform);
                }
            }
        }
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
