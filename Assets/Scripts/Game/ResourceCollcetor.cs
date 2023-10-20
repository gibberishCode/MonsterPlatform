using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;
using UnityEngine.Events;

public class ResourceCollcetor : MonoBehaviour
{
    [SerializeField] float CollectFrequency = 1;
    public UnityEvent<ResourceSpot> ResourceCollectedEvent;

    public int CollectedAtOnce = 1;
    private GameManager _gameManager;
    private List<ResourceSpot> _targets = new List<ResourceSpot>(); 
    private ResourceSpot _target;
    private ResourceSpot Target {
        get => _target;
        set {
            if (_target) {
                StopAllCoroutines();
            }
            _target = value;
            if (_target) {
                _target.Destroyed += FindNewTarget;
                StartCoroutine(StartCollecting());
            }

        }
    }

    private void Awake()
    {
        _gameManager = ServiceLocator.Current.Get<GameManager>();
    }

    private void OnEnable() {
        FindNewTarget();    
    }
    private void OnDisable() {
        StopAllCoroutines();
    }

    public void OnResourceInRange(Collider resource) {
        var target = resource.GetComponent<ResourceSpot>();
        if (Target == null && enabled) {
            Target = target;
        }  else {
            _targets.Add(target);
        }
    }

    public void OnResourceOutOfRange(Collider resource) {
        var target = resource.GetComponent<ResourceSpot>();
        _targets.Remove(target);
        if (Target == target) {
            FindNewTarget();
        }
    }

    private void FindNewTarget() {
        foreach (var target in _targets) {
            if (target) {
                Target = target;
                return;
            }
        } 
        Target = null;
    }

    private IEnumerator StartCollecting() {
        while (true) {
            yield return new WaitForSeconds(1.0f / CollectFrequency);
            Collect();
        }
    }

    private void Collect()
    {
        Debug.Log("Collecting");
        var spot = Target;
        Debug.Assert(spot);
        var collected = spot.Consume(CollectedAtOnce);
        _gameManager.GameData.Resources[(int)(spot.ResourceInfo.Type)].Amount += collected;
        ResourceCollectedEvent?.Invoke(spot);
    }
}
