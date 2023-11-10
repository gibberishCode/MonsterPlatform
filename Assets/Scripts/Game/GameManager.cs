using System;
using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] Player _player;
    [SerializeField] Platform _platform;
    [SerializeField] Joystick _joystick;
    [SerializeField] GameData _gameData;
    [SerializeField] UI _ui;
    [SerializeField] TowersSettings _towerSettings;
    [SerializeField] Camera _camera;
    [SerializeField] LayerMask _interactable;
    [SerializeField] GameSettings _settings;
    public LayerMask UILayer;
    public GameData GameData => _gameData;
    public Player Player => _player;
    public Platform Platform => _platform;
    public GameSettings GameSettings => _settings;
    public TowersSettings TowersSettings => _towerSettings;

    public Camera Camera => Camera.main;

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
    //Returns 'true' if we touched or hovering on Unity UI element.
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if ((1 << curRaysastResult.gameObject.layer & UILayer) != 0) {
                return true;
            }
        }
        return false;
    }
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
    private void ProcessClick()
    {
        EventSystem eventSystem = EventSystem.current;
        var overUI = IsPointerOverUIElement();
        if (overUI)
        {
            return;
        }
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        var hits = Physics.RaycastAll(ray, 1000);
        foreach (var hit in hits)
        {
            // var platformPiece = hit.collider.GetComponent<PlaftformPiece>();
            // if (platformPiece)
            // {
            //     _uiOptionsCreator.Open(_towerSettings.TowerBuildOptions, platformPiece.transform);
            // }
            // else
            var tower = hit.collider.GetComponentInParent<Tower>();
            if (!hit.collider.isTrigger  && tower)
            {
                _uiOptionsCreator.Open(_towerSettings.TowerUpgradeOptions, tower.transform);
                return;
            }
            else
            {
                var platformSpot = hit.collider.GetComponent<PlatformSpot>();
                if (platformSpot)
                {
                    // var options = new List<UIOptionData>(_towerSettings.TowerBuildOptions);
                    List<UIOptionData> options = new List<UIOptionData>();
                    foreach (var option in _towerSettings.TowerBuildOptions)
                    {
                        var clone = (UIOptionData)option.Clone();
                        // clone.Resources[0].Amount = 10000;
                        options.Add(clone);
                    }
                    _uiOptionsCreator.Open(options, platformSpot.transform);
                    return;
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
