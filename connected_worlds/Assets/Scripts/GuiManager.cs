using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Beardsoft.Singleton;

public class GuiManager : Singleton<GuiManager>
{

    public GameObject target;
    public GameObject starPrefab;
    public GameObject SpaceLane;
    public GameObject LineRendererPrefab;
    public GameObject ProductionFacilityPrefab;
    public GameObject MiningFacilityPrefab;

    public GUISkin guiSkin;
    public List<GameObject> systems;
    public Camera camera;
    public Texture productionIcon;
    public Texture miningIcon;

    private GUISkin currentSkin;
    private string text;
    private string objectName;
    private string objectType;
    private Texture objectTexture;

    public AudioSource audio; 
    public AudioClip clickSound;
    public AudioClip buildSound;

    private SystemController sysInfo;
    

    //menu vars
    private float menuBoxHeight = 140 * 1.3f;
    private float menuBoxWidth = 140 * 1.3f;
    private float menuBarHeight = 120 * 1.3f;
    private float menuBarWidth; // - 10;

    void Start()
    {
        //clickSource = clickAudio.GetComponent<AudioSource>();
        //buildSource = buildAudio.GetComponent<AudioSource>();
        audio = this.GetComponent<AudioSource>();
        currentSkin = guiSkin;
        text = "start";
    }


    void Awake()
    {
        base.Awake();
    }

    public void SwitchGui(GameObject obj)
    {
        audio.PlayOneShot(clickSound);
        if (obj.tag == "System")
        {
            //test
            sysInfo = obj.GetComponent<SystemController>();
            target = obj;
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
        if (Camera.main.fieldOfView < 60)
        {
            for (int i = 0; i < systems.Count; i++)
            {
                Vector3 size = systems[i].renderer.bounds.size;
                SystemController systemInfo = systems[i].GetComponent<SystemController>();
                Vector3 systemPos = camera.WorldToScreenPoint(systems[i].transform.position);
                float yPos = camera.pixelHeight - systemPos.y;
                string buildingType = systemInfo.buildingType;
                Texture icon = null;

                if (buildingType == "production")
                {
                    icon = productionIcon;
                } else if(buildingType == "mining") {
                    icon = miningIcon;
                }
                
                if (systemInfo.buildingType == "production")
                {
                    GUI.Label(new Rect(systemPos.x - (size.x) - 6, yPos - (size.y) - 33, 30, 30), icon, "systemName");
                }
                else if (systemInfo.buildingType == "mining")
                {
                    GUI.Label(new Rect(systemPos.x - (size.x) - 6, yPos - (size.y) - 33, 30, 30), icon, "systemName");
                }
                //GUI.Label(new Rect(systemPos.x , yPos - 50, 50, 25), " 5 ", "systemName");
                GUI.Label(new Rect(systemPos.x - 50, yPos + 20, 100, 25), systemInfo.name, "systemName");
            }
        }


        //draw main HUD

        menuBarWidth = Screen.width - menuBoxWidth;

        GUI.Box(new Rect(0 + menuBoxWidth, Screen.height - menuBarHeight, menuBarWidth, menuBarHeight), "");

        if (objectType == "System")
        {
            //if there isn't a battle here

            GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .8f) + 20, 100, 40), "Build Facilities", "blanklabel");
            GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .25f), Screen.height - (menuBoxHeight * .8f) + 20, 100, 40), "Build Space Lanes", "blanklabel");
            if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .25f), Screen.height - (menuBoxHeight * .5f) + 40 + 10, 200, 30), "Create a Space Lane -  50 metal"))
            {
                audio.PlayOneShot(clickSound);
                LaneBuilder laneBuilder = this.gameObject.AddComponent<LaneBuilder>();
                GameObject lineRenderer = Instantiate(LineRendererPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                laneBuilder.placeLaneStart(target, lineRenderer, SpaceLane);
            }

            if (sysInfo.buildingType == "")
            {
                if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 10, 200, 30), "Create a Factory -  50 metal"))
                {

                    sysInfo.buildBuilding("production");
                    audio.PlayOneShot(buildSound);

                    //objectTexture = productionIcon;

                }

                if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 40 + 10, 200, 30), "Create a Mine -  50 metal"))
                {
                    sysInfo.buildBuilding("mining");
                    audio.PlayOneShot(buildSound);
                }
            }
            else if (sysInfo.buildingType == "production")
            {
                if (sysInfo.buildingLevel == 0)
                {
                    GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 10, 200, 30), "Factory under construction", "blanklabel");
                }
                else
                {
                    GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 10, 200, 30), "building level : " + sysInfo.buildingLevel, "blanklabel");
                    if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 40 + 10, 200, 30), "Upgrade Factory - " + (sysInfo.buildingLevel * 5) + " metal"))
                    {
                        sysInfo.buildBuilding("mining");
                        audio.PlayOneShot(buildSound);
                    }

                    //DEBUG BUTTON
                    if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) - 100, 200, 30), "DEBUG ADD METAL"))
                    {
                        ProductionController prod = sysInfo.prodController;
                        prod.AddMetal(1);
                        Debug.Log("adding one metal");
                        Debug.Log("metal : " + prod.metal);
                        //audio.PlayOneShot(buildSound);
                    }
                }
            }
            else if (sysInfo.buildingType == "mining")
            {
                if (sysInfo.buildingLevel == 0)
                {
                    GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 10, 200, 30), "Mine under construction", "blanklabel");
                }
                else
                {
                    GUI.Label(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 10, 200, 30), "building level : " + sysInfo.buildingLevel, "blanklabel");
                    if (GUI.Button(new Rect(0 + menuBoxWidth + (menuBarWidth * .55f), Screen.height - (menuBoxHeight * .5f) + 40 + 10, 200, 30), "Upgrade Mine - " + (sysInfo.buildingLevel * 5) ))
                    {
                        sysInfo.buildBuilding("mining");
                        audio.PlayOneShot(buildSound);
                    }
                }
            }
                
        }

        GUI.Label(new Rect(0, Screen.height - menuBoxHeight, menuBoxWidth, menuBoxHeight), "");
        GUI.Label(new Rect(0 + 50, Screen.height - menuBoxHeight + 7, menuBoxWidth, 30), objectName, "blanklabel");
        GUI.DrawTexture(new Rect(0 + 41, Screen.height - menuBoxHeight + 55, 100, 100), objectTexture, ScaleMode.ScaleToFit, true, 0F);
        
        GUI.skin = null;
      
    }

    void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

}
