using UnityEngine;
using System.Collections;

[System.Serializable]  //bez ovoga se kod shop-a ne pojavljuje u inspectoru
public class TurretBlueprintScript
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost/2;
    }
}
