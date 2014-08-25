using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Beardsoft.Singleton;

public class GuiManager : Singleton<GuiManager>
{

    public GameObject target;
    public GameObject starPrefab;
    public GUISkin guiSkin;
    public List<GameObject> systems;
    public Camera camera;
    public Texture productionIcon;
    public Texture miningIcon;
    //public GUISkin laneSkin;
    //public GUISkin productionSkin;
    //public GUISkin miningSkin;

    private GUISkin currentSkin;
    private string text;
    private string objectName;
    private string objectType;
    private Texture objectTexture;

    private SystemControllerTest sysInfo;
    

    //menu vars
    private float menuBoxHeight = 140 * 1.3f;
    private float menuBoxWidth = 140 * 1.3f;
    private float menuBarHeight = 120 * 1.3f;
    private float menuBarWidth; // - 10;

    void Start()
    {
        
        currentSkin = guiSkin;
        text = "start";
    }


    void Awake()
    {
        base.Awake();
    }

    public void SwitchGui(GameObject obj)
    {
        if (obj.tag == "System")
        {
            //test
            sysInfo = obj.GetComponent<SystemControllerTest>();
            objectName = sysInfo.name;
            objectType = "System";
            objectTexture = sysInfo.renderer.material.GetTexture("_MainTex");
            /*
            if (sysInfo.hasBattle)
            {
                currentSkin = battleSkin;
            }
             * */
            //currentSkin = systemSkin;
        }
        else if (obj.tag == "SpaceLane")
        {
            //text = "SpaceLane";
            //currentSkin = laneSkin;
        }
        else if (obj.tag == "ProductionFacility")
        {
            //currentSkin = productionSkin;
        }
        else if (obj.tag == "MiningFacility")
        {
            //currentSkin = miningSkin;
        }

        currentSkin = guiSkin;
    }

    void OnGUI()
    {

        GUI.skin = currentSkin;

        //draw the labels of systems that are on screen
        if (camera.fieldOfView < 60)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                Vector3 size = systems[i].renderer.bounds.size;
                SystemControllerTest systemInfo = systems[i].GetComponent<SystemControllerTest>();
                Vector3 systemPos = camera.WorldToScreenPoint(systems[i].transform.position);
                float yPos = camera.pixelHeight - systemPos.y;

                if (systemInfo.buildingType == "production")
                {
                    GUI.Label(new Rect(systemPos.x - (size.x) - 6, yPos - (size.y) - 33, 30, 30), productionIcon, "systemName");
                }
                else if (systemInfo.buildingType == "mining")
                {
                    GUI.Label(new Rect(systemPos.x - (size.x) - 6, yPos - (size.y) - 33, 30, 30), productionIcon, "systemName");
                }
                //GUI.Label(new Rect(systemPos.x , yPos - 50, 50, 25), " 5 ", "systemName");
                GUI.Label(new Rect(systemPos.x - 50, yPos + 20, 100, 25), systemInfo.name, "systemName");
            }
        }


        //draw main HUD


        
        //GUI.skin = currentSkin
        menuBarWidth = Screen.width - menuBoxWidth;

        GUI.Box(new Rect(0 + menuBoxWidth, Screen.height - menuBarHeight, menuBarWidth, menuBarHeight), "");


        if (objectType == "System")
        {
            //if there isn't a battle here

            if (sysInfo.buildingType == "")
            {
                GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .8f) + 20, 100, 40), "Facilities", "blanklabel");
                if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 10, 200, 30), "Create a Factory -  50 metal"))
                {
                    Debug.Log("Building a fockin production facility");
                    sysInfo.buildBuilding("production");

                }

                if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 40 + 10, 200, 30), "Create a Mine -  50 metal"))
                {
                    Debug.Log("Building a fockin mining facility");
                    sysInfo.buildBuilding("mining");
                }
            }
            else if (sysInfo.buildingType == "production")
            {
                GUI.Label(new Rect(0, Screen.height - menuBoxHeight, menuBoxWidth, menuBoxHeight), "");
                //if (GUI.Button(new Rect(10, 70, 50, 30), ""))
                //{
                    //Debug.Log("Clicked the button with text");
               // }
            }
            else if (sysInfo.buildingType == "mining")
            {
                GUI.Label(new Rect(0, Screen.height - menuBoxHeight, menuBoxWidth, menuBoxHeight), "");
                //if (GUI.Button(new Rect(10, 70, 50, 30), ""))
                //{
                   // Debug.Log("Clicked the button with text");
                //}
            }
                
        }

        GUI.Label(new Rect(0, Screen.height - menuBoxHeight, menuBoxWidth, menuBoxHeight), "");
        GUI.Label(new Rect(0 + 50, Screen.height - menuBoxHeight + 7, menuBoxWidth, 30), objectName, "blanklabel");
        GUI.DrawTexture(new Rect(0 + 41, Screen.height - menuBoxHeight + 55, 100, 100), objectTexture, ScaleMode.ScaleToFit, true, 0F);
            //GUI.Box(new Rect(0 + 5, Screen.height - menuBoxHeight - 5, menuBoxWidth, menuBoxHeight), "");


        
        GUI.skin = null;
      
    }

    void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

}
