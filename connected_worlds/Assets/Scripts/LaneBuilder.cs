using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//using Beardsoft.Singleton;

public class LaneBuilder : MonoBehaviour
{
    private bool setup = false;
    private int maxRange = 5;
    private LineRenderer lineRenderer;
    private GameObject SpaceLane;
    private GameObject LineRendererObj;

    public GameObject firstSystem;
    public GameObject secondSystem;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
        if (setup)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //Debug.Log("Mousepos : " + mousepos.x + ", " + mousepos.y + ", " + mousepos.z);
            lineRenderer.SetPosition(1, mousepos);

            if (Input.GetMouseButtonDown(0))
            {
                mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));    
                //Vector3 castPos = GetWorldPositionOnPlane(mousepos, 0f);
                Debug.Log("Mousepos : " + mousepos.x + ", " + mousepos.y + ", " + mousepos.z);
                //Debug.Log("castPos : " + castPos.x + ", " + castPos.y + ", " + castPos.z);

                RaycastHit2D hit = Physics2D.CircleCast(mousepos, 1, Vector2.right);
                if (hit.transform != null)
                {
                    lineRenderer = null;
                    secondSystem = hit.transform.gameObject;
                    GameObject lane = Instantiate(SpaceLane, firstSystem.transform.position, Quaternion.identity) as GameObject;
                    LaneController laneController = lane.GetComponent<LaneController>();

                    SystemController sys1Controller = firstSystem.GetComponent<SystemController>();
                    SystemController sys2Controller = secondSystem.GetComponent<SystemController>();

                    laneController.system1 = sys1Controller;
                    laneController.system2 = sys2Controller;

                    sys1Controller.lanes.Add(sys2Controller, laneController);
                    sys2Controller.lanes.Add(sys1Controller, laneController);

                    Debug.Log(hit.transform.gameObject.name);
                    //lineRenderer.enabled = false;
                    setup = false;
                    Destroy(LineRendererObj);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //lineRenderer.enabled = false;
                setup = false;
                Destroy(LineRendererObj);
            }
        }
	}

    public void placeLaneStart(GameObject system, GameObject line, GameObject Lane)
    {
        SpaceLane = Lane;
        LineRendererObj = line;
        lineRenderer = line.GetComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(2);
        firstSystem = system;
        lineRenderer.SetPosition(0, firstSystem.transform.position);
        setup = true;
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
