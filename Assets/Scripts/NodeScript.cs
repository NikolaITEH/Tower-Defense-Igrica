using UnityEngine;

public class NodeScript : MonoBehaviour
{
    private Color startColor;   //pocetna boja
    public Color hoverColor;    // hover boja

    public Vector3 positionOffset;  //da bi se turret podigao za 0.5 na y osi

    private GameObject turret;  

    private Renderer rend;  //referenca za renderer za node

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;   //na mouseover se postavlja boja na hovercolor
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;   //kada se skloni mis sa node-a vraca se boja na prvobitnu
    }

    void OnMouseDown()
    {
        if (turret != null)     //ako vec postoji turret na node-u izlazi iz funkcije
        {
            Debug.Log("Can't build there!");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();        
        turret=Instantiate(turretToBuild,transform.position + positionOffset,transform.rotation);;  


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
