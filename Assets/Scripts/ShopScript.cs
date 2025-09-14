using UnityEngine;

public class ShopScript : MonoBehaviour
{

    public TurretBlueprintScript ballista;
    public TurretBlueprintScript cannon;

    BuildManager buildManager;

    public void SelectBallista()
    {
        buildManager.SelectTurretToBuild(ballista);
    }

    public void SelectCannon()
    {
        buildManager.SelectTurretToBuild(cannon);
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
