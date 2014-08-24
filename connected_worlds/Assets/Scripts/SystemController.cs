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
	public List<LaneController> lanes = new List<LaneController>();

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
			Debug.Log (gameObject.name + " has " + metalPerTurn + " metal. finding a route to get some back from.");
			//We need to loop through all our lanes and see if we're sending metal from them
			for(int i =0; i < lanes.Count; i++){
				float sentMetal = lanes[i].GetMetalFromSystem(gameObject.name);
				if(sentMetal > 0){
					Debug.Log("looks like " + lanes[i].gameObject.name + " is taking " + sentMetal + " from us. Going to take " + Mathf.Min (sentMetal, -metalPerTurn) + " back.");
					lanes[i].RemoveFromRoute(gameObject.name, Mathf.Min(sentMetal, -metalPerTurn), 0f);
				}
				if(metalPerTurn >=0){
					Debug.Log("We're all good now.");
					return;
				}
			}
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
		CheckResources();
	}

	#endregion
}
