using UnityEngine;
using System.Collections;

public class LaneController : MonoBehaviour {

	#region Properties & Variables

	public GameObject planet1;
	public GameObject planet2;
	public int upgradeLevel = 0;

	private float metalEnRoute;
	private float millitaryEnRoute;
	private GameObject startSystem;
	private GameObject endSystem;

	#endregion

	#region MonoBehavior Implementation
	
	void Start () {
		startSystem = planet1;
		endSystem = planet2;
	}

	void Update () {
	
	}

	void OnDrawGizmos(){
		if(startSystem != null && endSystem != null){
			Gizmos.color = Color.red;
			Gizmos.DrawLine(startSystem.transform.position, endSystem.transform.position);
		}
	}

	#endregion
}
