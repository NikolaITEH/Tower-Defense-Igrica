using UnityEngine;

public class ShopScript : MonoBehaviour
{

    BuildManager buildManager;

    public void PurchaseBallista()
    {
        buildManager.SetTurretToBuild(buildManager.ballistaPrefab);
    }

    public void PurchaseCannon()
    {
        buildManager.SetTurretToBuild(buildManager.cannonPrefab);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
