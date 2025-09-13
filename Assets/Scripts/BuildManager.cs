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

    public GameObject standardTurretPrefab;
    

    private GameObject turretToBuild;   //trenutno selektovani turret

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
