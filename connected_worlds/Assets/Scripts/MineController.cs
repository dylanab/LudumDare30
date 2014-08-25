using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineController : MonoBehaviour {

    //private int metal;
    public int level;
    public bool mine;
    //private int upgradePool;


    //public int metalToUpgrade;
    //public bool upgrading = false;
    public GameObject MetalShipPrefab;
    public SystemController thisSystem;
    public SystemController tarSystem;
    public GameObject tarFacility;
    public List<SystemController> route;

    void Awake()
    {
        mine = false;
    }

	// Use this for initialization
	void Start () {

        TimeCounter.ProductionTick += this.ProduceMetal;
        level = 1;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (upgrading)
        {
            if (upgradePool == metalToUpgrade)
            {
                level += 1;
                Debug.Log("production facility upgrade complete!");
                upgrading = false;
            }
        }
         * */
    }

    public void ProduceMetal()
    {
        
        if (mine) {
            for (int i = 0; i < level; i++)
            {
                Debug.Log("tarFacility" + tarFacility.name + " , " + "tarSystem" + tarSystem.name + " , " + "thisSystem" + thisSystem.name);
                GameObject metalShit = Instantiate(MetalShipPrefab, this.gameObject.transform.position, Quaternion.identity) as GameObject;
                metalShit.GetComponent<MetalNavigation>().Activate(tarFacility, tarSystem, thisSystem, route);
            }
        }
    }

    public void SetupRoute(SystemController thisSystem, SystemController otherSystem)
    {
        Debug.Log("routing from : " + thisSystem.name + " to " + otherSystem.name);
        route = GraphTraveler.FindRoute(thisSystem, otherSystem); 
    }

    /*
    public void AddMetal(int metalCount)
    {
        if (upgrading)
        {
            upgradePool += metalCount;
        }
        else
        {
            metal += metalCount;
        }
        
    }
     * 
    public void UpgradeBuilding()
    {
        metalToUpgrade = (level + 1) * 5;
        upgrading = true;
        
    }
     * */
  
}
