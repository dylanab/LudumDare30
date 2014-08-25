using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugGuy : MonoBehaviour {

	public SystemController start;
	public SystemController end;

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			DebugRoute();
		}
	}

	void DebugRoute(){
		List<SystemController> r = GraphTraveler.FindRoute(start, end);
		if(r == null)
			Debug.Log ("ERROR");
		Debug.Log ("Route from " + start.name + " to " + end.name + ": ");
		for(int i = 0; i < r.Count; i++){
			Debug.Log ("Step " + i+1 + ": " + r[i].name);
		}
	}
}
