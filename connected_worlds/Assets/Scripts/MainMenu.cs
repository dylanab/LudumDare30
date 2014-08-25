using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public float buttonWidth = 0f;
	public float buttonHeight = 0f;

	public string startButtonText = "";
	public string exitText = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(GUI.Button(new Rect((float)(Screen.width/2f) - (buttonWidth/2f), (float)(Screen.height/2f) - (buttonHeight * 1.1f), buttonWidth, buttonHeight), startButtonText)){
			Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect((float)(Screen.width/2f) - (buttonWidth/2f), (float)(Screen.height/2f) + (buttonHeight * 0.1f), buttonWidth, buttonHeight), exitText)){
			Application.Quit();
		}
	}
}
