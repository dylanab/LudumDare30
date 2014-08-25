using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Beardsoft.Singleton;

public class LaneBuilder : Singleton<LaneBuilder>
{
    private bool setup = false;
    private int maxRange = 5;
    private LineRenderer lineRenderer;
    private GameObject SpaceLane;

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
                mousepos.z = 0f;
                RaycastHit2D hit = Physics2D.CircleCast(mousepos, 1, Vector2.right);
                if (hit.transform != null)
                {
                    secondSystem = hit.transform.gameObject;
                    GameObject lane = Instantiate(SpaceLane, firstSystem.transform.position, Quaternion.identity) as GameObject;
                    LaneController laneController = lane.GetComponent<LaneController>();
                    //laneController.system1 = firstSystem.gameObject;

                    Debug.Log(hit.transform.gameObject.name);
                }
                
            }
        }
	}

    public void placeLaneStart(GameObject system, LineRenderer line, GameObject Lane)
    {
        SpaceLane = Lane;
        lineRenderer = line;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(2);
        firstSystem = system;
        lineRenderer.SetPosition(0, firstSystem.transform.position);
        setup = true;
    }
}
