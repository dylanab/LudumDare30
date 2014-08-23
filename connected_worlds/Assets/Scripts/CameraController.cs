using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public float deadZone = .1f;
	public float moveSpeed = 10;
	public float pixelBuffer = 50f;

	private BoxCollider2D box;
	private List<GameObject> systems = new List<GameObject>();

	// Use this for initialization
	void Start () {
		box = GetComponent<BoxCollider2D>();
		box.size = new Vector2((camera.pixelWidth + pixelBuffer) / 100, (camera.pixelHeight + pixelBuffer) / 100);

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
			Debug.Log (systems.Count);
			SystemController systemInfo = systems[i].GetComponent<SystemController>();
			Vector3 systemPos = camera.WorldToScreenPoint(systems[i].transform.position);
			float metal = systemInfo.GetMetalPerTurn();
			float yPos = camera.pixelHeight - systemPos.y;
			GUI.Box (new Rect(systemPos.x, yPos, 100, 25), "fgfgf" + metal);
		}
	}
}
