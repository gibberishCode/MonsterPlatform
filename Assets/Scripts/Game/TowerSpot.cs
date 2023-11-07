using System;
using System.Collections;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;
using UnityEngine.VFX;

public class TowerSpot : MonoBehaviour
{
    [SerializeField] List<Tower> TowerPrefabs = new List<Tower>();

    [SerializeField] Material _tryingToBuildMaterial;
    public event Action<TowerSpot> PlayerIn;
    public event Action<TowerSpot> PlayerOut;
    private ResourceManager _resoruceManager;
    private Material _normal;

    private void Awake()
    {
        _resoruceManager = ServiceLocator.Current.Get<ResourceManager>();
        _normal = GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
     
        var player = other.GetComponentInParent<TowerBuilder>();
        if (player)
        {
            PlayerIn?.Invoke(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponentInParent<TowerBuilder>();
        if (player)
        {
            PlayerOut?.Invoke(this);
        }

    }

    private bool EnoughResources()
    {
        var resources = _resoruceManager.Resources;
        return resources[0].Amount >= 5 && resources[1].Amount >= 5;
    }

    public IEnumerator Build()
    {
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(EnoughResources);
        _resoruceManager.Resources[0].Amount -= 5;
        _resoruceManager.Resources[1].Amount -= 5;
        var prefab = TowerPrefabs.GetRandom();
        var tower = Instantiate(prefab, transform.parent);
        tower.transform.position = transform.position;
        tower.transform.rotation = transform.rotation;
        Destroy(gameObject);
    }

    internal void TryingToBuild(bool state)
    {
        StopAllCoroutines();
        GetComponent<Renderer>().material = state ? _tryingToBuildMaterial : _normal;
    }
}
