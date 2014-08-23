﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public float deadZone = .1f;
	public float moveSpeed = 10;
	public float pixelBuffer = 50f;

	private BoxCollider2D box;
	private List<GameObject> systems = new List<GameObject>();

	//GUI STYLE VARIABLES
	GUIStyle style;
	Texture2D tex;

	// Use this for initialization
	void Start () {
		style = new GUIStyle();
		tex = new Texture2D(1,1);
		tex.SetPixel(1, 1, Color.white);

		box = GetComponent<BoxCollider2D>();
        box.size = new Vector2((camera.pixelWidth + pixelBuffer) / 50, (camera.pixelHeight + pixelBuffer) / 50);//new Vector2((camera.pixelWidth + pixelBuffer) / 100, (camera.pixelHeight + pixelBuffer) / 100);

	}
	
	// Update is called once per frame
	void Update () {
		float h = 0f;
		float v = 0f;
		if(Mathf.Abs (Input.GetAxis("Horizontal")) > deadZone){
			h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		}
		if(Mathf.Abs (Input.GetAxis("Vertical")) > deadZone){
			v = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		}
		transform.position += new Vector3(h, v, 0f);
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "System"){
			systems.Add(collider.gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D collider){
		if(collider.tag == "System"){
			systems.Remove(collider.gameObject);
		}
	}

	void OnGUI(){
		for(int i = 0; i < systems.Count; i++){
			SystemController systemInfo = systems[i].GetComponent<SystemController>();
			Vector3 systemPos = camera.WorldToScreenPoint(systems[i].transform.position);
			float metal = systemInfo.GetMetalPerTurn();
			float units = systemInfo.GetUnitsPerTurn();
			float yPos = camera.pixelHeight - systemPos.y;
<<<<<<< HEAD
			GUI.Box (new Rect(systemPos.x - 50, yPos - 50, 100, 25), "metal : " + metal);
=======

			style.normal.background = tex;
			GUI.color = Color.cyan;
			GUI.Box (new Rect(systemPos.x, yPos, 150, 25), "Metal: " + metal + " Units: " + units, style);
>>>>>>> origin/master
		}

        //draw larger menu 
        float menuBoxHeight = 140 * 1.3f;
        float menuBoxWidth = 140 * 1.3f;
        float menuBarHeight = 120 * 1.3f;
        float menuBarWidth = Screen.width - menuBoxWidth - 10;
        GUI.Box(new Rect(0 + 5, Screen.height - menuBoxHeight - 5, menuBoxWidth, menuBoxHeight), "TEST");
        GUI.Box(new Rect(0 + 5 + menuBoxWidth + 1, Screen.height - menuBarHeight - 5, menuBarWidth, menuBarHeight), "TEST");
	}

}
