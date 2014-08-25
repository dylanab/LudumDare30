using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugGuy : MonoBehaviour {

	public SystemController start;
	public SystemController end;

	public GameObject ship;

	void Start(){
	}
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
		Vector3 noise = new Vector3(Random.Range(0f, .1f), Random.Range(0f, .1f), 0f);
		GameObject testShip = (GameObject)Instantiate(ship, start.transform.position + noise, Quaternion.identity);

		testShip.GetComponent<MetalNavigation>().Activate(null, end, start, r);
	}

	void DebugTick(){
	}
}
