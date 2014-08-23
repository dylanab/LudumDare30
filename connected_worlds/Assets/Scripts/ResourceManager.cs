using UnityEngine;
using System.Collections;
using Beardsoft.Singleton; 

public class ResourceManager : Singleton<ResourceManager> {

    public float turnInterval = 2.0f; 
    private float currentTime; 
    private bool gameIsPaused;

    private float BankedMetal;
    private float TotalUnits;

    #region Event Signals
        //Event to tell systems to extract metal  
        public delegate void ExtractMetalSignal();
        public static event ExtractMetalSignal ExtractMetal;

        //Event to tell systems to produce units 
        public delegate void ProduceUnitsSignal();
        public static event ProduceUnitsSignal ProduceUnits;

        //Event to tell systems to give their metal to the resource manager 
        public delegate void GiveMetalSignal();
        public static event GiveMetalSignal GiveMetal;

    #endregion

    #region Private Methods 

        void Start()
        {
            BankedMetal = 0;
            currentTime = 0;
            gameIsPaused = false;
            InvokeRepeating("Tick", initDelay, turnDelay);
        }

        void Update()
        {
            if (!gameIsPaused)
            {
              currentTime += Time.deltaTime;
              if (currentTime >= turnInterval)
              {
                 Tick();
                 currentTime = 0;
              }
            } 
        }

        //send signals to all systems telling them to do all the stuff they need to do each turn
        private void Tick()
        {
            if (ExtractMetal != null)
            {
                ExtractMetal();
            }

            if (ProduceUnits != null)
            {
                ProduceUnits();
            }

            if (GiveMetal != null)
            {
                GiveMetal();
            }
        }

        private void PauseGame()
        {

        }

    #endregion 

    #region Public Interface

        //Sends a signal to all the systems to give their surplus metal to the resource manager
        public static void SendMetal()
        {
            if (GiveMetal != null)
            {
                GiveMetal();
            }
        }

        public void AddMetal(float metalToAdd)
        {
            if (metalToAdd >= 0)
            {
                BankedMetal += metalToAdd;
            }
        }

        public float GetBankedMetal()
        {
            return BankedMetal; 
        }

        //Add a unit to total units
        public void AddUnits(int unitsToAdd)

    #endregion


}
