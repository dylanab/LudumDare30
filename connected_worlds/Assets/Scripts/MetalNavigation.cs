using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MetalNavigation : MonoBehaviour {

	#region Properties & Variables

	//State variables
	private string STATE_INIT = "Init";
	private string STATE_ORBITING = "Orbiting";
	private string STATE_TRAVELING = "Traveling";
	private string currentState ="";


	NavigationParameters nav = new NavigationParameters();

	/** //Flags
	public bool _waiting;		//Whether the ship is currently in a queue to jump (so it can ignore move ticks)
	public bool _activated;	//Flag to tell the ship all of its parameters have been set, if this is false most checks will not run
	public bool _atNext;		//Whether or not the ship is at the system set to currentSystem
	public bool _arrived;		//Whether or not the ship has reached it's end destination
	public bool _arrivalInfo;	//Whether or not the ship has sent the arrival information to the target facility or system
	**/

	//Movement variables
	public float speed = 2f;			//The speed at which the ship moves
	public float turnSpeed = 2f;		//The speed at which the ship rotates
	public int metal = 1;				//The amount of metal to deliver

	//Route variables (this is where it gets hairy) 0.o
	 List<SystemController> route;		//A list of systems from current to target

	SystemController lastSystem = null;	//The last system the ship was at
	SystemController currentSystem;		//The current system the ship is at
	SystemController nextSystem;			//The next system to jump to
	SystemController targetSystem;		//The end goal system
		
	int nextSystemIndex = 0;			//The index of the route the next system is at
	LaneController nextLane;			//The next lane to queue in to get to nextSystem
	GameObject targetFacility;			//The facility (if any) that the ship is trying to reach

	#endregion

	#region MonoBehavior Implementation
	// Use this for initialization
	void Awake() {
		nav._waiting = false;		//Whether the ship is currently in a queue to jump (so it can ignore move ticks)
		nav._activated = false;	//Flag to tell the ship all of its parameters have been set, if this is false most checks will not run
		nav._atNext = false;		//Whether or not the ship is at the system set to currentSystem
		nav._arrived = false;		//Whether or not the ship has reached it's end destination
		nav._arrivalInfo = false;
		Debug.Log("AWAKE");
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(nav._waiting);
		if(currentState == STATE_ORBITING){
			ProcessOrbiting();
		}
		if(currentState == STATE_TRAVELING){
			ProcessTraveling();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(nav._activated){
			if(collider.gameObject == nextSystem.gameObject){
				rigidbody2D.velocity = Vector2.zero;
				nav._atNext = true;
			}
		}
	}

	#endregion

	#region FSM Functions

	public void StartFSM(){
		currentState = STATE_INIT;
	}

	public void StartOrbiting(){
		Debug.Log ("STAR ORBIT");
		currentState = STATE_ORBITING;
	}
	public void StartTraveling(){
		currentState = STATE_TRAVELING;
	}

	#endregion

	#region State processing Functions
	private IEnumerator ProcessInit(){
		while(!nav._activated){
			
			yield return null;
		}
		StartOrbiting();
	}

	public void ProcessOrbiting(){
		Debug.Log ("Orbiting");
		if(!nav._waiting){
			StartTraveling();
		}
		else{
			transform.RotateAround(currentSystem.transform.position, new Vector3(0f, 0f, 1f), turnSpeed);
		}
	}

	private void ProcessTraveling(){
		Debug.Log ("Traveling");
		if(nav._atNext){
			currentSystem = nextSystem;
			nav._waiting = true;
			if(currentSystem == targetSystem){
				nav._arrived = true;
				if(targetFacility != null){
					//targetFacility.AddMetal(metal);
					Destroy (this.gameObject);
				}
				else{
					targetSystem.AddShip(this.gameObject);
				}
			}
			else{
				nextSystemIndex++;
				nextSystem = route[nextSystemIndex];
				nextLane = currentSystem.lanes[nextSystem];
				AddToQueue();
			}
			nav._atNext = false;
			StartOrbiting();
		}
		else{
			Vector3 targetVector = nextSystem.transform.position - transform.position;
			rigidbody2D.velocity = new Vector2(targetVector.x, targetVector.y).normalized * speed;
		}
	}

	#endregion

	private void AddToQueue(){
		nextLane.AddToQueue(this.gameObject);
	}

	#region Public Interface

	public bool GetActivated(){ return (nav._activated == true); }

	public void Activate(GameObject tarFacility, SystemController tarSystem, SystemController thisSystem, List<SystemController> r){
		if(currentSystem != null)
			Debug.Log(currentSystem.name);
		//Set variables to passed parameters
		targetFacility = tarFacility;
		targetSystem = tarSystem;
		currentSystem = thisSystem;
		route = r;

		//Check to make sure the starting system isn't the target system
		if(currentSystem == targetSystem){
			//If it is then we set all these to true so the  ship sends it arrival info on next tick
			Debug.Log ("Created me at the target system");
			nav._arrived = true;
			nav._activated = true;
			nav._waiting = true;
		}
		//If we aren't at the target systm
		if(!nav._arrived){
			//calculate rest of variables based of given parameters
			nextSystem = route[nextSystemIndex];
			nextLane = currentSystem.lanes[nextSystem];
			AddToQueue();
			nav._waiting = true;
			nav._activated = true;
		}
		nav._activated = true;
		StartOrbiting();
		Debug.Log("current state: " + currentState);
	}
	//Moves the ship through the lane it was waiting on maybe not needed?
	public void EndWait(){
		if(nav._waiting)
			nav._waiting = false;
	}

	#endregion


}

[SerializeField]
public class NavigationParameters{

	public bool _waiting;		//Whether the ship is currently in a queue to jump (so it can ignore move ticks)
	public bool _activated;	//Flag to tell the ship all of its parameters have been set, if this is false most checks will not run
	public bool _atNext;		//Whether or not the ship is at the system set to currentSystem
	public bool _arrived;		//Whether or not the ship has reached it's end destination
	public bool _arrivalInfo;	//Whether or not the ship has sent the arrival information to the target facility or system

}
