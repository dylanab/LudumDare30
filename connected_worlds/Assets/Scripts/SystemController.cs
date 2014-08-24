using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemController : MonoBehaviour {

	#region Properties & Variables

	public string name = "";
	public int owner = 0;

	public float metalPerTurn = 0f;
	public float unitsPerTurn = 0f;
	private bool slotFilled = false;

	//Update Time variables (used for debug, won't be located here in final version)
	private float updateInterval = 3f;
	private float currenTime = 0f;

	//List of all lanes connected to this system
	private List<LaneController> lanes = new List<LaneController>();

	#endregion

	#region MonoBehvior Implementation
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		currenTime += Time.deltaTime;
		if(currenTime >= updateInterval){
			UpdateTime();
			currenTime = 0;
		}
	}


	#endregion

	#region Utility Funtions

	private void UpdateTime(){

	}

	private void CheckResources(){
		CheckMetal();
		CheckUnits();
	}

	private void CheckMetal(){
		if(metalPerTurn < 0){	//Metal per turn is below zero so we're sending more than we have 

		}
	}

	private void CheckUnits(){

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

	#endregion
}
