using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectUIScript : MonoBehaviour
{

    public GameObject ui;

    private NodeScript target;

    public TextMeshProUGUI upgradeCost;

    public Button upgradeButton;


    public void SetTarget(NodeScript _target)
    {
        target= _target;
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost + " gold";
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "Max!";
            upgradeButton.interactable = false;
        }



            ui.SetActive(true);
    }


    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
