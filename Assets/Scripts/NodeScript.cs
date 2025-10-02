using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    private Color startColor;   //pocetna boja
    public Color hoverColor;    // hover boja
    public Color notEnoughGoldColor;

    public Vector3 positionOffset;  //da bi se turret podigao za 0.5 na y osi


    [Header("Optional")]
    public GameObject turret;
    public TurretBlueprintScript turretBlueprint;
    public bool isUpgraded = false;

    private Renderer rend;  //referenca za renderer za node

    public Vector3 GetBuildPosition()
    {
        return transform.position+positionOffset;
    }


    void BuildTurret(TurretBlueprintScript blueprint)
    {
        if (PlayerStatsScript.gold < blueprint.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        PlayerStatsScript.gold -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBlueprint = blueprint;

        GameObject effect = Instantiate(BuildManager.instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret built!");
    }

    private void OnMouseEnter()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!BuildManager.instance.CanBuild)   //highlight-ovanje samo kad je selektovan neki turret
        {
            return;
        }

        if (BuildManager.instance.HasGold)
        {
            rend.material.color = hoverColor;   //na mouseover se postavlja boja na hovercolor
        }else
        {
            rend.material.color = notEnoughGoldColor;
        }

    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;   //kada se skloni mis sa node-a vraca se boja na prvobitnu
    }

    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        if (turret != null)     //ako vec postoji turret na node-u izlazi iz funkcije
        {
            BuildManager.instance.SelectNode(this);
            return;
        }

        if (!BuildManager.instance.CanBuild)   //ako nije selektovan turret, izadji iz funk.
        {
            return;
        }

        BuildTurret(BuildManager.instance.GetTurretToBuild());

    }

    public void UpgradeTurret()
    {
        if (PlayerStatsScript.gold < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        PlayerStatsScript.gold -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = Instantiate(BuildManager.instance.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;
        Debug.Log("Turret upgraded!");
    }

    public void SellTurret()
    {
        PlayerStatsScript.gold +=turretBlueprint.GetSellAmount();

        GameObject effect = Instantiate(BuildManager.instance.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);

        turretBlueprint=null;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend=GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
