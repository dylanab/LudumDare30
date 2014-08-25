using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RouteLinker : MonoBehaviour
{
    private int maxRange = 5;
    private LineRenderer lineRenderer;
    private SystemController firstSystem;
    private SystemController secondSystem;
    private GameObject LineRendererObj;
    private bool setup = false;

    

	// Use this for initialization
	void Start () {

	}

    void Update()
    {
        if (setup)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            lineRenderer.SetPosition(1, mousepos);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.CircleCast(mousepos, 1, Vector2.right);
                if (hit.transform != null)
                {
                    lineRenderer = null;
                    secondSystem  = hit.transform.gameObject.GetComponent<SystemController>();
                    if (secondSystem.buildingType == "production")
                    {
                        firstSystem.mineController.SetupRoute(firstSystem, secondSystem);
                    }

                    Destroy(LineRendererObj);
                    setup = false;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                setup = false;
                Destroy(LineRendererObj);
            }
        }
    }

    public SystemController SystemAtPos()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        RaycastHit2D hit = Physics2D.CircleCast(mousepos, 1, Vector2.right);
        if (hit.transform != null)
        {
            lineRenderer = null;
            GameObject System = hit.transform.gameObject;
            SystemController sysController = System.GetComponent<SystemController>();
            if (sysController.buildingType == "production")
            {
                return sysController;
            }

            Destroy(LineRendererObj);
        }
        return null;
    }

    public void placeLineStart(GameObject system, GameObject line)
    {
        firstSystem = system.GetComponent<SystemController>();
        LineRendererObj = line;
        lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        Color c1 = Color.green;
        Color c2 = Color.green;
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(2);
        lineRenderer.SetPosition(0, system.transform.position);
        setup = true;
    }

    /*
    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }*/
}
