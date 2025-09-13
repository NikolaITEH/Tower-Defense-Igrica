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

    public GameObject ballistaPrefab;
    public GameObject cannonPrefab;
    

    private GameObject turretToBuild;   //trenutno selektovani turret

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
