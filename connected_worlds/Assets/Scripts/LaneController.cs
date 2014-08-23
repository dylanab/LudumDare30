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

	private bool _activated = false;
	//DEBUG
	private bool _set = false;

	#endregion

	#region MonoBehavior Implementation

	void Start () {
		//DEBUG -Cory
		startSystemInfo = startSystem.GetComponent<SystemController>();
		endSystemInfo = endSystem.GetComponent<SystemController>();
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
	}

	void OnDrawGizmos(){
		if(startSystem != null && endSystem != null){
			Gizmos.color = Color.red;
			Gizmos.DrawLine(startSystem.transform.position, endSystem.transform.position);
		}
	}

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
		}

		//Reset variables to new values
		startSystem = startSys;
		endSystem = endSys;
		startSystemInfo = startSystem.GetComponent<SystemController>();
		endSystemInfo = endSystem.GetComponent<SystemController>();
		metalEnRoute = metal;
		unitsEnRoute = units;
		_activated = true;

		//Route the new resources to their target
		startSystemInfo.RemoveResources(metalEnRoute, unitsEnRoute);
		endSystemInfo.AddResources(metalEnRoute, unitsEnRoute);
	}

	#endregion
}
