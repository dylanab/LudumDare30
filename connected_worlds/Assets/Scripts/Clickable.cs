using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour {

    private GameObject gui;
    private GuiManager guiManager;
    private GameObject target;

	// Use this for initialization
	void Start () {
        gui = GameObject.FindGameObjectWithTag("GuiContainer");
        target = null;
        guiManager = gui.GetComponent<GuiManager>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnMouseDown() {
        //if (guiManager.target.name != this.gameObject.name)
        //{
            target = this.gameObject;
            guiManager.SwitchGui(target);
        //}
    }
}
