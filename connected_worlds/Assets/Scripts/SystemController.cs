using UnityEngine;
using System.Collections;

public class SystemController : MonoBehaviour {

	#region Properties & Variables

	public int owner = 0;

	public float metalPerTurn = 0f;
	public float unitsPerTurn = 0f;
	private bool slotFilled = false;

	//Update Time variables (used for debug, won't be located here in final version)
	private float updateInterval = 3f;
	private float currenTime = 0f;

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
