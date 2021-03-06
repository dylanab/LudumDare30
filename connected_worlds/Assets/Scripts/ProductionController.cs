﻿using UnityEngine;
using System.Collections;

public class ProductionController : MonoBehaviour
{

    public int metal;
    public int level;
    public int upgradePool;

    public AudioSource audio;
    public AudioClip resourceSound;

    public int metalToUpgrade;
    public bool upgrading = false;

    // Use this for initialization
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        TimeCounter.ProductionTick += this.ProduceShips;

        metal = 0;
        upgradePool = 0;
        level = 0;
        upgrading = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("Trying to produce ships");
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

        audio.PlayOneShot(resourceSound);

        //TODO: play the resource sound; 

    }

    public void UpgradeBuilding()
    {
        metalToUpgrade = (level + 1) * 5;
        upgrading = true;
    }

}
