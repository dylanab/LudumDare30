using UnityEngine;
using System.Collections;

[RequireComponent (typeof(LineRenderer))]

public class LaneController : MonoBehaviour {

	#region Properties & Variables

	public GameObject startSystem;
	public GameObject endSystem;
	public int upgradeLevel = 0;

	private float metalEnRoute;
	private float unitsEnRoute;
	private SystemController startSystemInfo;
	private SystemController endSystemInfo;

	private LineRenderer line;
	private bool _activated = false;
	//DEBUG
	private bool _set = false;

	#endregion

	#region MonoBehavior Implementation

	void Start () {
		//DEBUG -Cory
		startSystemInfo = startSystem.GetComponent<SystemController>();
		endSystemInfo = endSystem.GetComponent<SystemController>();
		//Cache LineRenderer
		line = GetComponent<LineRenderer>();
	}

	//DEBUG -Cory
	void Update () {
		if(Input.GetButtonDown("Jump")){
			if(_set){
				DeActivateRoute();
				_set = false;
			}
			else{
				SetRoute(startSystem, endSystem, startSystemInfo.GetMetalPerTurn(), startSystemInfo.GetUnitsPerTurn());
				_set = true;
			}
		}
		line.SetPosition(0, startSystem.transform.position);
		line.SetPosition(1, endSystem.transform.position);
	}

	void OnDrawGizmos(){
		if(startSystem != null && endSystem != null){
			Gizmos.color = Color.red;
			Gizmos.DrawLine(startSystem.transform.position, endSystem.transform.position);
		}
	}

	#endregion

	#region Getters and Setters (or mutators and accessors if you're a fuckin' nerd)

	public float GetMetalEnRoute(){ return metalEnRoute; }
	public float GetUnitsEnRoute(){ return unitsEnRoute; }


	#endregion

	#region Public Interface

	public void DeActivateRoute(){
		if(_activated){
			endSystemInfo.RemoveResources(metalEnRoute, unitsEnRoute);
			startSystemInfo.AddResources(metalEnRoute, unitsEnRoute);
			_activated = false;
		}
	}

	public void SetRoute(GameObject startSys, GameObject endSys, float metal, float units){
		//First return whatever was being transported before if activated
		if(_activated){
			endSystemInfo.RemoveResources(metalEnRoute, unitsEnRoute);
			startSystemInfo.AddResources(metalEnRoute, unitsEnRoute);
			endSystemInfo.ResourceCheck();
		}

		//Reset variables to new values
		startSystem = startSys;
		endSystem = endSys;
		startSystemInfo = startSystem.GetComponent<SystemController>();
		endSystemInfo = endSystem.GetComponent<SystemController>();
		metalEnRoute = metal;
		unitsEnRoute = units;
		if(metalEnRoute == 0 && unitsEnRoute == 0)
			_activated = false;
		else
			_activated = true;

		//Route the new resources to their target
		startSystemInfo.RemoveResources(metalEnRoute, unitsEnRoute);
		endSystemInfo.AddResources(metalEnRoute, unitsEnRoute);
	}

	#endregion
}
