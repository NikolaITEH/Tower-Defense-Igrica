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

    public GameObject sellEffect;
    

    private TurretBlueprintScript turretToBuild;   //trenutno selektovani turret
    private NodeScript selectedNode;

    public SelectUIScript selectUI;

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

    public void SelectTurretToBuild(TurretBlueprintScript turret)
    {
        turretToBuild = turret;
        DeselectNode();
       

    }

    public void SelectNode(NodeScript node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild=null;
        selectUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        selectUI.Hide();
    }

    public TurretBlueprintScript GetTurretToBuild()
    {
        return turretToBuild;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
