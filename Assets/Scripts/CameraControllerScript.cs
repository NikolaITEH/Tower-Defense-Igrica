using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{

    public float panSpeed = 30;
    public float panBorderThickness = 10;
    private bool movement = true;
    public float scrollSpeed = 5;
    public float minY = 10;
    public float maxY = 80;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFunctionalityScript.gameIsOver)
        {
            this.enabled= false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            movement = !movement;
        }

        if (!movement)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);   //Vector3.forward je Vector3(0, 0, 1)
            //bez space.world kad kliknemo W kamera ce ici ka dole jer prati njenu rotaciju
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);   
            
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);   
                
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);   
            
        }

        float scroll=Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y=Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
