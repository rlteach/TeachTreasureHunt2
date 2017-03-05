using UnityEngine;
using System.Collections;


namespace Multiplayer {

    public class GM : RL_Helpers.Singleton {

        static GM sGM;      //used to link to Singleton

        void Awake() {
            if(CreateSingleton<GM>(ref sGM)) {

            }
        }

        #region Player
        PlayerController mLocalPlayer;      //Local player gets stored here

        static  public  PlayerController    LocalPlayer {

            set {
                sGM.mLocalPlayer = value;
            }
            get {
                return  sGM.mLocalPlayer;
            }
        }
        #endregion


        #region Inventory

        InventoryPanel mInventoryPanel;

        static public InventoryPanel InventoryPanel {

            set {
                sGM.mInventoryPanel = value;
            }
            get {
                return sGM.mInventoryPanel;
            }
        }
        #endregion
    }
}
