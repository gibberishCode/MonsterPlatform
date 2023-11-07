using System;
using System.Collections.Generic;
using MyUnityHelpers;
using UnityEngine;

 interface IUIOption {
     void Select(Transform caller);
}

[Serializable]
public abstract class UIOptionData : IUIOption
{
    public Sprite Icon;
    public Color Color = Color.white;
    public string Title;
    public string Text;
    public List<ResourceInfo> Resources;
    public abstract void Select(Transform caller);
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
        var platformPice = caller.GetComponent<PlaftformPiece>();
        Debug.Assert(platformPice);
        platformPice.Build(TowerPrefab);
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
public class TowersSettings : ScriptableObject {
    public Tower ResourceTowerPrefab;
    public Tower AttackTowerPrefab;
    public PlaftformPiece PlaftformPiecePrefab;
    [SubclassSelector, SerializeReference]
    public List<UIOptionData> PlatformBuildOptions;
    [SubclassSelector, SerializeReference]
    public List<UIOptionData> TowerBuildOptions;
    
}