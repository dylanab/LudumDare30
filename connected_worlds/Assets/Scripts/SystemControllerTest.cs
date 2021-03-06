﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemControllerTest : MonoBehaviour {

	#region Properties & Variables

	public int owner = 0;

	public float metalPerTurn = 0f;
	public float unitsPerTurn = 0f;

    //Diff
    public string buildingType = "";
    public int buildingLevel = 0;
	private bool slotFilled = false;
    public string name; 

	//Update Time variables (used for debug, won't be located here in final version)
	private float updateInterval = 3f;
	private float currenTime = 0f;

	//List of outgoing and incoming lanes
	private List<LaneController> outgoingLanes = new List<LaneController>();
	private List<LaneController> incomingLanes = new List<LaneController>();


	#endregion

	#region MonoBehvior Implementation
	// Use this for initialization
	void Start () {
        //DIFF
        name = "Tau Ceti " + Random.Range(0, 5000);
	}
	
	// Update is called once per frame
	void Update () {
		currenTime += Time.deltaTime;
		if(currenTime >= updateInterval){
			UpdateTime();
			currenTime = 0;
		}
	}

    public void buildBuilding(string type)
    {
        //TODO: some way to
        if (type == "production")
        {
            //Instantiate(productionPrefab, this.gameObject.transform.position, Quaternion.identity);
            buildingType = "production";
            buildingLevel = 1;
        }
        else if (type == "mining")
        {
            //Instantiate(miningPrefab, this.gameObject.transform.position, Quaternion.identity);
            buildingType = "mining";
            buildingLevel = 1;
        }
    }


	#endregion

	#region Utility Funtions

	private void UpdateTime(){

	}

	#endregion

	#region Public Interface

	public float GetMetalPerTurn(){ return metalPerTurn; }

	public float GetUnitsPerTurn(){ return unitsPerTurn; }

	public void AddResources(float metal, float units){
		metalPerTurn += metal;
		unitsPerTurn += units;
	}

	public void RemoveResources(float metal, float units){
		metalPerTurn -= metal;
		unitsPerTurn -= units;
	}
	//Add and remove functions of outgoing lane list
	public void AddOutgoingLane(LaneController lane){ outgoingLanes.Add(lane); }
	public void RemoveOutgoingLane(LaneController lane){ outgoingLanes.Remove(lane); }

	//Add and remove functions for incoming lane list
	public void AddIncomingLane(LaneController lane){ incomingLanes.Add(lane); }
	public void RemoveIncomingLane(LaneController lane){ incomingLanes.Remove(lane); }

	public void ResourceCheck(){
		//Check to see if metal/turn < 0
		if(metalPerTurn < 0){
			//Loop through all outgoing lanes
			for(int i = 0; i < outgoingLanes.Count; i++){
				//cache current lane and current lane's metal amount
				LaneController currentLane = outgoingLanes[i];
				//float currentMetal = currentLane.GetMetalEnRoute();
				//if(currentMetal > 0){
					//currentLane.SetRoute(this.gameObject, currentLane.endSystem, Mathf.Max(currentMetal + metalPerTurn, 0f), currentLane.GetUnitsEnRoute());
				//}

			}
		}
	}

	#endregion
}
