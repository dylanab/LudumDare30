using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MetalNavigation : MonoBehaviour {

	#region Properties & Variables

	private GameObject targetFacility;
	private SystemController targetSystem;
	private List<SystemController> route = new List<SystemController>();
	private SystemController currentSystem;
	private int nextLaneIndex = 0;
	private SystemController nextSystem;
	private LaneController nextLane;

	private bool _arrived = false;
	private bool inLane = false;
	private bool inSys = true;

	private float tickTime;				//Time that travel between systems should take
	private bool _waiting = false;		//Tells the ship not to continue along its destination
	private bool _activated = false;	//Tells the ship not to attempt navigation until activated using the Activate() function
	private string owner;				//"Player" if player built this ship or "Enemy" if the AI built this ship

	#endregion

	#region MonoBehavior Implementation
	// Use this for initialization
	void Start (){

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#endregion

	private void TakeMove(){
		if(_waiting){}
		else if(_arrived){
			if(targetFacility != null){
				//targetFacility.AddMetal(metal); UNCOMMENT WHEN DYLANS FACILITY CODE IS ADDED IN
				Destroy (this.gameObject);
			}
			else
			{}
		}
	}

	#region Public Interface

	public void Activate(GameObject tarFacility, SystemController tarSystem, SystemController thisSystem, List<SystemController> r){
		targetFacility = tarFacility;
		targetSystem = tarSystem;
		currentSystem = thisSystem;
		owner = thisSystem.GetOwner();
		route = r;
		nextSystem = r[nextLaneIndex];
		nextLane = currentSystem.lanes[nextSystem];
		nextLaneIndex++;

		_activated = true;
	}

	//Moves the ship through the lane it was waiting on
	public void EndWait(){
		if(_waiting)
			_waiting = false;
	}

	#endregion
}
