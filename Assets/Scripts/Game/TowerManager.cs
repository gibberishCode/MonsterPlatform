using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManagerData {
    public float TowerAttackMultiplies = 1;
    public float TowerHealthMultiplies = 1;

}

public class TowerManager : MonoBehaviour
{
    public TowerManagerData TowerManagerData {get;set;}

}
