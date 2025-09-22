using UnityEngine;

public class SelectUIScript : MonoBehaviour
{

    public GameObject ui;

    private NodeScript target;
    public void SetTarget(NodeScript _target)
    {
        target= _target;
        transform.position = target.GetBuildPosition();
        ui.SetActive(true);
    }


    public void Hide()
    {
        ui.SetActive(false);
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
