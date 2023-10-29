using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IUpgrade {
    void Apply(IUpgradeTarget target);
}

public interface IUpgradeTarget { 
    
}

public abstract class PlayerUpgrade : IUpgrade
{
    public float Percentage;
    public abstract void Apply(IUpgradeTarget target);
}

[Serializable]
public class PlayerHealthUpgrade : PlayerUpgrade
{
    public override void Apply(IUpgradeTarget target)
    {
        var player = (Player)target;
        player.PlayerData.HealthMultiplier += Percentage;
    }
}

public abstract class TowerUpgrade : IUpgrade
{
    public float Percentage;
    public abstract void Apply(IUpgradeTarget target);
}
[Serializable]
public class AttackTowerUpgrade : TowerUpgrade
{
      public override void Apply(IUpgradeTarget target)
    {
        var towerManager = (TowerManager)target;
        towerManager.TowerManagerData.TowerAttackMultiplies += Percentage;
    }
}
[Serializable]
public class HealthTowerUpgrade : TowerUpgrade
{
    public override void Apply(IUpgradeTarget target)
    {
        throw new System.NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "UpgradesData", menuName = "MonsterPlatform/UpgradesData", order = 0)]
public class UpgradesData : ScriptableObject {
    [SerializeReference, SubclassSelector]
	List<IUpgrade> _upgrades = new List<IUpgrade>();

}