using UnityEngine;

public class ShopScript : MonoBehaviour
{

    public TurretBlueprintScript ballista;
    public TurretBlueprintScript cannon;
    public TurretBlueprintScript catapult;

    BuildManager buildManager;

    public void SelectBallista()
    {
        buildManager.SelectTurretToBuild(ballista);
    }

    public void SelectCannon()
    {
        buildManager.SelectTurretToBuild(cannon);
    }

    public void SelectCatapult()
    {
        buildManager.SelectTurretToBuild(catapult);
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
