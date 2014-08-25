using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public float deadZone = .1f;
	public float moveSpeed = 10;
	public float pixelBuffer = 50f;
    public float scrollSpeed = 50f;

    public GUISkin spaceLaneSkin;
    public GUISkin systemLabelSkin;

    private GameObject gui;
    private GuiManager guiManager; 

	private BoxCollider2D box;
	private List<GameObject> systems = new List<GameObject>();

	// Use this for initialization
	void Start () {

        gui = GameObject.FindGameObjectWithTag("GuiContainer");
        guiManager = gui.GetComponent<GuiManager>();

		box = GetComponent<BoxCollider2D>();
        box.size = new Vector2((50) , (50) );//new Vector2((camera.pixelWidth + pixelBuffer) / 100, (camera.pixelHeight + pixelBuffer) / 100);

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

        /* 
        if (Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")) > 0f)
        {
            if (Camera.main.fieldOfView < 80 && Camera.main.fieldOfView > 20)
            {
                float camNearClip = Camera.main.nearClipPlane;
                //float camFOV = Camera.main.fieldOfView;
                float zoom = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed ; //*scrollSpeed * Time.deltaTime;
                Camera.main.fieldOfView += zoom;
                Camera.main.nearClipPlane -= zoom;
                
            }
        }
         * */

        //update the visible object lists in the gui manager
        guiManager.systems = systems;
        guiManager.camera = camera;

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


    /*
	void OnGUI(){
        /*
		for(int i = 0; i < systems.Count; i++){
			SystemControllerTest systemInfo = systems[i].GetComponent<SystemControllerTest>();
			Vector3 systemPos = camera.WorldToScreenPoint(systems[i].transform.position);
			float metal = systemInfo.GetMetalPerTurn();
            float units = systemInfo.GetUnitsPerTurn();
			float yPos = camera.pixelHeight - systemPos.y;
            GUI.skin = systemLabelSkin; 
			GUI.Box (new Rect(systemPos.x - 50, yPos - 50, 120, 25), "metal : " + metal + " units : " + units);
            GUI.skin = null; 
            GUI.Label(new Rect(systemPos.x - 50, yPos + 20, 100, 25), systemInfo.getName());
		}

        //draw larger menu 
        /*
        float menuBoxHeight = 140 * 1.3f;
        float menuBoxWidth = 140 * 1.3f;
        float menuBarHeight = 120 * 1.3f;
        float menuBarWidth = Screen.width - menuBoxWidth - 10;
        GUI.Box(new Rect(0 + 5, Screen.height - menuBoxHeight - 5, menuBoxWidth, menuBoxHeight), "TEST");
        GUI.skin = spaceLaneSkin; 
        GUI.Box(new Rect(0 + 5 + menuBoxWidth + 1, Screen.height - menuBarHeight - 5, menuBarWidth, menuBarHeight), "TEST");
        GUI.skin = null;
         * */
	//}
    
}
