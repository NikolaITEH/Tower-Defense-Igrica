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

    private Renderer rend;  //referenca za renderer za node

    public Vector3 GetBuildPosition()
    {
        return transform.position+positionOffset;
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

        if (!BuildManager.instance.CanBuild)   //ako nije selektovan turret, izadji iz funk.
        {
            return;
        }

        if (turret != null)     //ako vec postoji turret na node-u izlazi iz funkcije
        {
            Debug.Log("Can't build there!");
            return;
        }

        BuildManager.instance.BuildTurretOn(this);


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
