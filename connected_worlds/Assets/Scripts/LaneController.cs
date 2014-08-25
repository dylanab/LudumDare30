using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(LineRenderer))]

public class LaneController : MonoBehaviour {

	#region Properties & Variables

	//Upgrade variables
	public int upgradeLevel = 1;
	public int maxUpgradeLevel = 5;

    public GameObject LineRendererPrefab;
    private GameObject lineRendererObj;
    private LineRenderer line;
    private bool lineSetup = false;

	private int currentMetal = 0;	//Might store this in a LaneUpgrade object or something
	private int nextUpgradeMetal;	//^^ Ditto ^^
    

	//Not sure if we need these
	public SystemController system1;
	public SystemController system2;

	//Queue of all ships waiting for signal to move through the lane
	private Queue<GameObject> waitingShips = new Queue<GameObject>();

	#endregion

	#region MonoBehavior Implementation

	void Start () {
		//TO DO: Subscribe MoveShips to some tick event
	}
	
	void Update () {
        
        if (system1 != null && system2 != null && !lineSetup)
        {
            lineRendererObj = Instantiate(LineRendererPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
            line = lineRendererObj.GetComponent<LineRenderer>();
            //line.material = new Material(Shader.Find("Particles/Additive"));
            //lineRenderer.SetColors(c1, c2);
            line.SetWidth(0.07F, 0.07F);
            line.SetVertexCount(2);
            Vector3 pos1 =  system1.transform.position;
            Vector3 pos2 = system2.transform.position;
            pos1.z = pos2.z = .4f;
            line.SetPosition(0, pos1);
            line.SetPosition(1, pos2);
            lineSetup = true;
        }
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine(system1.gameObject.transform.position, system2.gameObject.transform.position);
	}

	#endregion

	#region Transportation functions

	public void AddToQueue(GameObject newShip){
		waitingShips.Enqueue(newShip);
		Debug.Log("Just added " + newShip.name + " to queue.");
		Debug.Log ("Waiting Ships: " + waitingShips.Count);
	}

	private void MoveShips(){
		for(int i = 0; i < upgradeLevel; i++){
			GameObject nextShip = waitingShips.Dequeue();
			if(nextShip.tag == "Metal")
				nextShip.GetComponent<MetalNavigation>().EndWait();
			else
				nextShip.GetComponent<WarshipNavigation>().EndWait();
		}
	}

	#endregion

	#region Getters and Setters (or mutators and accessors if you're a fuckin' nerd)

	public int GetUpgradeLevel(){ return upgradeLevel; }
	public int GetNextUpgradeMetal(){ return nextUpgradeMetal; }


	#endregion

	#region Public Interface
	
	public SystemController GetOther(string name){
		if(system1.name == name)
			return system2;
		else
			return system1;
	}
	public int GetTraffic(){
		return waitingShips.Count;
	}

	public void Upgrade(){
		upgradeLevel += 1;
		nextUpgradeMetal = 10*upgradeLevel;	//Might be removed
		currentMetal = 0;					//Might be removed
	}





	#endregion
}
