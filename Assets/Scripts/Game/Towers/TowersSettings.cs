using System;
using System.Collections.Generic;
using System.Data;
using MyUnityHelpers;
using UnityEngine;

interface IUIOption
{
    void Select(Transform caller);
}

[Serializable]
public abstract class UIOptionData : IUIOption, ICloneable
{
    public Sprite Icon;
    public Color Color = Color.white;
    public string Title;
    public string Text;
    [SerializeField]
    private List<ResourceInfo> _resources;

    public object Clone()
    {
        return MemberwiseClone();
    }

    public abstract void Select(Transform caller);
    public List<ResourceInfo> GetResources() {
        List<ResourceInfo> resources = new List<ResourceInfo>();
        var gameManager = ServiceLocator.Current.Get<GameManager>();
        var platform = gameManager.Platform;
        var gameSettings = gameManager.GameSettings;
        foreach(var r in _resources) {
            resources.Add(new ResourceInfo(r.Amount * Mathf.Pow(gameSettings.TowerCostRate, platform.PlatformPices), r.Type));
        }
         return resources;
    }
}

[Serializable]
public class BuildPlatform : UIOptionData
{
    public override void Select(Transform caller)
    {
        var platformPice = caller.GetComponent<PlatformSpot>();
        Debug.Assert(platformPice);
        platformPice.Build();
    }
}

[Serializable]
public class BuildTower : UIOptionData
{
    public Tower TowerPrefab;
    public override void Select(Transform caller)
    {
        var platformSpot = caller.GetComponent<PlatformSpot>();
        Debug.Assert(platformSpot);
        platformSpot.BuildWithTower(TowerPrefab);
        var uiOptionsCreator = ServiceLocator.Current.Get<UIOptionsCreator>();
        uiOptionsCreator.Close();
    }
}

public interface ITowerUpgrade {
    void Apply(Tower target);
}

[Serializable]
public class HealthUpgrade : ITowerUpgrade
{
    public float Multiplier = 0.1f;
    public void Apply(Tower target)
    {
        var damageable = target.GetComponent<Damageable>();
        Debug.Assert(damageable);
        damageable.AddToMulitiplier(Multiplier);
    }
}

[Serializable]
public class DamageUpgrade : ITowerUpgrade
{
    public float Multiplier = 0.1f;
    public void Apply(Tower target)
    {
        var tower = target.GetComponent<AttackTower>();
        Debug.Assert(tower);
        tower.AddToMultiplier(Multiplier);
    }
}


[Serializable]
public class UpgradeOption : UIOptionData
{
    [SerializeReference, SubclassSelector] 
    public ITowerUpgrade _upgrade;
    public override void Select(Transform caller)
    {
        var tower = caller.GetComponent<Tower>();
        Debug.Assert(tower);
        _upgrade.Apply(tower);
        var uiOptionsCreator = ServiceLocator.Current.Get<UIOptionsCreator>();
        uiOptionsCreator.Close();
    }
}

[Serializable]
public class Cancel : UIOptionData
{
    public override void Select(Transform caller)
    {
        var uiOptionsCreator = ServiceLocator.Current.Get<UIOptionsCreator>();
        uiOptionsCreator.Close();
    }
}


[CreateAssetMenu(fileName = "TowersSettings", menuName = "MonsterPlatform/TowersSettings", order = 0)]
public class TowersSettings : ScriptableObject
{
    public Tower ResourceTowerPrefab;
    public Tower AttackTowerPrefab;
    public PlaftformPiece PlaftformPiecePrefab;
    [SubclassSelector, SerializeReference]
    public List<UIOptionData> PlatformBuildOptions;
    [SubclassSelector, SerializeReference]
    public List<UIOptionData> TowerBuildOptions;
    [SubclassSelector, SerializeReference]
    public List<UIOptionData> TowerUpgradeOptions;

}