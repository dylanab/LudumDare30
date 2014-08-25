using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Navigation : MonoBehaviour {

	#region Properties & Variables

	private GameObject targetFacility;
	private SystemController targetSystem;
	private List<LaneController> route = new List<LaneController>();
	private SystemController currentSystem;
	private int nextLane = 0;

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

	#region Public Interface

	public void Activate(GameObject tarFacility, SystemController tarSystem, SystemController thisSystem){
		targetFacility = tarFacility;
		targetSystem = tarSystem;
		currentSystem = thisSystem;
		owner = thisSystem.GetOwner();

		_activated = true;
	}

	public void endWait(){

	}

	#endregion
}
