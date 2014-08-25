using UnityEngine;
using System.Collections;

public class MineController : MonoBehaviour {

    private int metal;
    private int level;
    private int upgradePool;

    public int metalToUpgrade;
    public bool upgrading = false;

	// Use this for initialization
	void Start () {

        TimeCounter.ProductionTick += this.ProduceShips;

        metal = 10;
        upgradePool = 0;
        level = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (upgrading)
        {
            if (upgradePool == metalToUpgrade)
            {
                level += 1;
                Debug.Log("production facility upgrade complete!");
                upgrading = false;
            }
        }
    }

    public void ProduceShips()
    {
        if (!upgrading)
        {
            for (int i = 0; i < level; i++)
            {
                if (metal > 0)
                {
                    //Use some of our some metal 
                    metal -= 1;
                    //Spawn a Warship
                    Debug.Log("Spawing warship");
                }
            }
        }
    }

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

        //TODO: play the resource sound; 
        
    }

    public void UpgradeBuilding()
    {
        metalToUpgrade = (level + 1) * 5;
        upgrading = true;
        
    }
  
}
