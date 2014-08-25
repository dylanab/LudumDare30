using UnityEngine;
using System.Collections;

using Beardsoft.Singleton;

public class InputManager : Singleton<InputManager> 
{

    #region Event Signals
        //Event for player touching screen (mouse left click on PC)
        public delegate void TouchSignal(bool touching);
        public static event TouchSignal SetTouching;
        //Event for adding force to player controller
        public delegate void AddForceSignal(Vector2 force);
        public static event AddForceSignal AddForce;
        //Event for removing force from the player controller
        public delegate void RemoveForceSignal(Vector2 force);
        public static event RemoveForceSignal RemoveForce;
        //Event for adding chi to player's max chi
        public delegate void AddChiSignal(float chiIncrease);
        public static event AddChiSignal AddPlayerChi;

    #endregion

    #region Monbehavior Implementation

        void Awake()
        {
            base.Awake();
        } 
	
        //Read key input from player. Likely a better solution than checking once per update?
	    void Update () {
	        if(Input.GetButtonDown("Follow"))
            {
                if(SetTouching != null)
                {
                    SetTouching(true);
                }
            }
            if(Input.GetButtonUp("Follow"))
            {
                if(SetTouching != null)
                {
                    SetTouching(false);
                }
            }
	    }

        void OnApplicationQuit()
        {
            base.OnApplicationQuit();
        }

    #endregion

    #region Public Interface

        //Sends a signal to player controller that adds a force to its list of applied forces
        public static void SendForce(Vector2 force)
        {
            if(AddForce != null)
                AddForce(force);
        }
        //Sends a signal to player controller that removes a vector from its current list of applied forces
        public static void TakeForce(Vector2 force)
        {
            if(RemoveForce != null)
                RemoveForce(force);
        }
        //Send a signal to player that adds chiIncrease to their max chi value
        public static void AddChi(float chiIncrease)
        {
            if(AddPlayerChi != null)
                AddPlayerChi(chiIncrease);
        }

    #endregion
    }

