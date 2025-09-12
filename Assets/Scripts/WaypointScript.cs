using UnityEngine;

public class WaypointScript : MonoBehaviour
{

    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];

        //Popunjavamo points niz sa waypoint-ovima

        for (int i = 0; i < points.Length; i++)
        {
            points[i]=transform.GetChild(i);
        }
    }


}
