﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class LaneController : MonoBehaviour {

	#region Properties & Variables
	//Determines if the route has be properly set up for use
	private bool _activated = false;

	//Stores the gameobjects of the systems this route lies between
	public GameObject system1;
	public GameObject system2;
	private SystemController system1Info;
	private SystemController system2Info;

	//Upgrade and capacity for transport
	public float transportCapacity = 10f;
	public int upgradeLevel = 0;

	//Route from system1 to system 2 variables
	private float metalFrom1 = 0f;
	private float unitsFrom1 = 0f;

	//Route from system2 to system1
	private float metalFrom2 = 0f;
	private float unitsFrom2 = 0f;

	#endregion

	#region MonoBehavior Implementation

	void Start () {
		ActivateRoute(system1, system2);
	}

	//DEBUG - Cory
	void Update () {
		if(_activated){
			if(Input.GetButtonDown("Jump")){
				float randomMetal = Mathf.Ceil(Random.Range(0f, 10f));
				float randomUnits = Mathf.Ceil(Random.Range (0f, 10f));
				string randomTarget = "";
				if(Random.Range(0f, 1f) > .5f)
					randomTarget += system1.name;
				else
					randomTarget += system2.name;
				Debug.Log ("Adding " + randomMetal + " metal and " + randomUnits + " units from " + randomTarget);
				AddToRoute(randomTarget, randomMetal, randomUnits);
			}
			if(Input.GetButtonDown("Fire1")){
				Debug.Log ("En Route from " + system1.name + " to " + system2.name + ": Metal: " + metalFrom1 + " Units: " + unitsFrom1 + ". ");
				Debug.Log ("En Route from " + system2.name + " to " + system1.name + ": Metal: " + metalFrom2 + " Units: " + unitsFrom2 + ". ");
			}
		}
	}

	void OnDrawGizmos(){

	}

	#endregion

	#region Getters and Setters (or mutators and accessors if you're a fuckin' nerd)

	//Returns the metal along the route from the system given by systemName
	public float GetMetalFromSystem(string systemName){
		if(systemName == system1.name)
			return metalFrom1;
		else if(systemName == system2.name)
			return metalFrom2;
		else{
			Debug.LogError("Tried to get metal from a route that the specified system is not part of");
			return -1;
		}
	}

	//Returns the units along the route from the system given by systemName
	public float GetUnitsFromSystem(string systemName){
		if(systemName == system1.name)
			return unitsFrom1;
		else if(systemName == system2.name)
			return unitsFrom2;
		else{
			Debug.LogError("Tried to get units from a route that the specified system is not part of");
			return -1;
		}
	}

	#endregion

	#region Public Interface

	//Used to activate this route for use upon completion
	public void ActivateRoute(GameObject startSystem, GameObject endSystem){
		system1 = startSystem;
		system1Info = startSystem.GetComponent<SystemController>();
		system2 = endSystem;
		system2Info = endSystem.GetComponent<SystemController>();
		//Set activated flag
		_activated = true;
	}
	//Used to add resources to the route from the specified system
	public void AddToRoute(string startSystemName, float metal, float units){
		if(startSystemName == system1.name){
			system1Info.RemoveResources(metal, units);
			metalFrom1 += metal;
			unitsFrom1 += units;
			system2Info.AddResources(metal, units);
		}
		else if(startSystemName == system2.name){
			system2Info.RemoveResources(metal, units);
			metalFrom2 += metal;
			unitsFrom2 += units;
			system1Info.AddResources(metal, units);
		}
		else
			Debug.LogError("Tried to add resources to a route that the given system is not part of");
	}



	#endregion
}
