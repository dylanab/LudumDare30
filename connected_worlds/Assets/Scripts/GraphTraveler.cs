using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Beardsoft.Singleton;

public class GraphTraveler : Singleton<GraphTraveler> {

	void Awake(){
		base.Awake();
	}

	void OnApplicationQuit(){
		base.OnApplicationQuit();
	}

	public static List<SystemController> FindRoute(SystemController startSystem, SystemController endSystem){
		//Create List to store result
		List<SystemController> route = new List<SystemController>();
		//Create open and closed lists to store systems that have been examined
		Dictionary<SystemController, float> openSet = new Dictionary<SystemController, float>();							//Open set -Systems that have been given an f value but not yet visited
		//---------This system, parent system
		Dictionary<SystemController, SystemController> closedSet = new Dictionary<SystemController, SystemController>();	//Closed set -Systems that have been visited and found to be potential optimal paths

		bool done = false;								//Boolean to determine when we've found the target
		SystemController currentSystem = startSystem;	//The system currently being examined
		SystemController targetSystem = endSystem;		//The system we're searching for
		SystemController previousSystem;

		//Add startSystem to open set and set its A* variables
		float h_distanceCost = FindDistance(currentSystem.gameObject, targetSystem.gameObject);
		float g_movementCost = 0f;
		openSet.Add(currentSystem, h_distanceCost + g_movementCost);
		closedSet.Add(currentSystem, null);

		int counter = 0;
		//Contine to search until we've found the target
		while(!done){
			counter++;
			//If there are no system in the open set return null and throw error
			if(openSet.Count == 0){
				Debug.LogError ("Could not find route to target");
				return null;
			}
			//Find the system in the open set with the lowest f score
			currentSystem = FindLowest(openSet);
			//Add the system to the closed set
			if(currentSystem != startSystem){
				closedSet.Add(currentSystem, currentSystem.parent);
				openSet.Remove(currentSystem);
			}
			else
				openSet.Remove(currentSystem);
			//If this system is the target system exit loop
			if(currentSystem == targetSystem){
				done = true;
			}
			//Loop through each system the current system is connected to
			foreach(var sys in currentSystem.lanes.Keys){
				//If this system is in the closed set move on
				if(closedSet.ContainsKey(sys)){}
				//Otherwise calculate new f score
				float newF = FindDistance(sys.gameObject, targetSystem.gameObject) + currentSystem.lanes[sys].GetTraffic() + 1;
				//If sys is not in the open set, add it to the open set
				if(!openSet.ContainsKey(sys)){
					openSet.Add(sys, newF);
					sys.parent = currentSystem;
				}
				//Otherwise if sys is in open set and newF < its f value replace it
				else if(openSet[sys] > newF){
					openSet.Remove(sys);
					openSet.Add(sys, newF);
					sys.parent = currentSystem;
				}
			}
		}
		//currentSystem is the targetSystem right now add currentSystem to the return list
		do{
			route.Add(currentSystem);
			currentSystem = closedSet[currentSystem];
		}while(closedSet[currentSystem] != null);

		route.Reverse();

		return route;
	}

	private static float FindDistance(GameObject start, GameObject end){
		return (start.transform.position - end.transform.position).magnitude;
	}

	private static void DebugPrint(List<SystemController> list){

	}

	private static SystemController FindLowest(Dictionary<SystemController, float> dic){
		float lowest = 9999f;
		SystemController result = null;
		foreach(var key in dic.Keys){
			if(dic[key] < lowest){
				lowest = dic[key];
				result = key;
			}
		}
		return result;
	}
}
