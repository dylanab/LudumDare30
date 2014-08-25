using UnityEngine;
using System.Collections;

using Beardsoft.Singleton;

public class TimeCounter : Singleton<TimeCounter> {

	private const float MOVE_TICK_TIME = 4f;
	private const float PRODUCTION_TICK_TIME = 4f;

	private float currentMoveTimer = 0f;
	private float currentProductionTimer = 0f;

	public delegate void MoveSignal();
	public static event MoveSignal MoveTick;

	public delegate void ProductionSignal();
	public static event ProductionSignal ProductionTick;

	void Awake(){
		base.Awake();
		currentMoveTimer += 2f;
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		currentMoveTimer += Time.deltaTime;
		currentProductionTimer += Time.deltaTime;
		if(currentMoveTimer >= MOVE_TICK_TIME){
			SignalMoveTick();
			currentMoveTimer = 0f;
		}
		if(currentProductionTimer >= PRODUCTION_TICK_TIME){
			SignalProductionTick();
			currentProductionTimer = 0f;
		}
	}

	void OnApplicationQuit(){
		base.OnApplicationQuit();
	}

	private void SignalMoveTick(){
		if(MoveTick != null)
			MoveTick();
	}

	private void SignalProductionTick(){
		if(ProductionTick != null)
			ProductionTick();
	}
}
