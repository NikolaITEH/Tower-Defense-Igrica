using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance; 
    //da bi mogli pristupiti buildmanager-u bilo gde
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }


    public GameObject buildEffect;
    

    private TurretBlueprintScript turretToBuild;   //trenutno selektovani turret

    public bool CanBuild
    {
        get
        {
            return turretToBuild != null;
        }
    }

    public bool HasGold
    {
        get
        {
            return PlayerStatsScript.gold >= turretToBuild.cost;
        }
    }

    public void BuildTurretOn(NodeScript node)
    {
        if (PlayerStatsScript.gold < turretToBuild.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        PlayerStatsScript.gold-=turretToBuild.cost;

        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        GameObject effect = Instantiate(buildEffect, node.GetBuildPosition() , Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built! Gold left: " + PlayerStatsScript.gold);
    }

    public void SelectTurretToBuild(TurretBlueprintScript turret)
    {
        turretToBuild = turret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
