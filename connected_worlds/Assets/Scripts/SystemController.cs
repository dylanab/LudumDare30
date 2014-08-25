using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemController : MonoBehaviour {

	#region Properties & Variables

	//Variables used in the A* algorithm, otherwise ignored by this object
	public float g_movementCost = 0f;
	public float h_distanceCost = 0f;
	public SystemController parent;

    public string buildingType = "";
    public int buildingLevel = 0;
    private bool slotFilled = false;
    public string name; 

	public List<LaneController> debugLanes = new List<LaneController>();
	public Dictionary<SystemController, LaneController> lanes = new Dictionary<SystemController, LaneController>();
	public string owner = "Player"; //DEBUG HARD CODED VALUE

    public GameObject ProductionFacilityPrefab;
    public GameObject MiningFacilityPrefab;
    public GameObject facility;
    public ProductionController prodController;
    public MineController mineController;

    private List<MetalNavigation> metalHere = new List<MetalNavigation>();
    private List<WarshipNavigation> warshipsHere = new List<WarshipNavigation>();

	#endregion

	#region MonoBehvior Implementation

	void Start () {
        
		for(int i = 0; i < debugLanes.Count; i++){
			lanes.Add (debugLanes[i].GetOther(this.name), debugLanes[i]);
		}
	}

	void Update () {
		//DEBUG
        if (Input.GetKeyDown(KeyCode.Space) && facility != null)
        {
            prodController.AddMetal(1);
        }
	}


	#endregion

	#region Utility Funtions



	#endregion

	#region Getters and Setters


	#endregion

	#region Public Interface

	//Remove a ship from this system's list of ships
	public void RemoveShip(GameObject ship){
		if(ship.tag == "Metal")
			metalHere.Remove(ship.GetComponent<MetalNavigation>());
		else if(ship.tag == "Warship")
            warshipsHere.Remove(ship.GetComponent<WarshipNavigation>());
		else
			Debug.LogError("Tried to remove a ship from " + this.name + " that is not a recognized ship type.");
	}

	//Add a ship to this system's list of ships
	public void AddShip(GameObject ship){
		if(ship.tag == "Metal")
            metalHere.Add(ship.GetComponent<MetalNavigation>());
		else if(ship.tag == "Warship")
            warshipsHere.Add(ship.GetComponent<WarshipNavigation>());
		else
			Debug.LogError("Tried to add a ship to " + this.name + " that is not a recognized ship type.");
	}

	//Add a lane to this system's list of connected lanes
	public void AddLane(LaneController lane){
	}

    /*
	//Set a facility to this object's facility slot
	public void SetFacility(GameObject newFacility){
		if(facility == null)
			facility = newFacility;
		else
			Debug.LogError ("Tried to build a facility on " + this.name + " where a facility already exists.");
	}
     * */

	//Return the string representing the owner of this system
	public string GetOwner(){ return owner; }


    public void buildBuilding(string type)
    {
        if (type == "production" && buildingType == "")
        {
            Debug.Log("Building factory");
            facility = Instantiate(ProductionFacilityPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
            prodController = facility.GetComponent<ProductionController>();
            buildingType = "production";
            //DEBUG: Normally this would be set to 0
            buildingLevel = prodController.level = 1;
        }
        else if (type == "mining" && buildingType == "")
        {
            facility = Instantiate(MiningFacilityPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
            buildingType = "mining";
            //DEBUG: Normally this would be set to 0
            buildingLevel = prodController.level = 1;
        }
    }

	#endregion
}
