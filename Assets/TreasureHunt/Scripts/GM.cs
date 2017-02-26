using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : Singleton {       //Derive from Singleton, makes a global static

    #region Debug
    //Sets global debug output flag, true means show messages
    public static bool DebugOn = true;

    string mDebugText = "hello";

    public static void Msg(string vDebugMsg) {
        if (DebugOn) {
            Debug.Log(vDebugMsg);
        }
    }
    void ClearDebugText() {
        mDebugText = "";
    }
    static public string DebugText {
        get {
            return sGM.mDebugText;
        }

        set {
            sGM.mDebugText = value;
            sGM.Invoke("ClearDebugText", 3f);
        }
    }

    
    #endregion

    #region Singleton
    private static GM sGM;        //Used to keep singleton reference
    void Awake() {
        if (CreateSingleton(ref sGM)) {

        }
    }
    #endregion


	#region SpawnManagment
	ObjectSpawner	mOS;

	public	static	ObjectSpawner OS {
		get {
			return	sGM.mOS;
		}
		set {
			sGM.mOS = value;
		}
	}

	#endregion

    #region PlayerManagement
    private List<PlayerController> mPlayer = new List<PlayerController>();      //Keep list of Players in game
                                                                                //Add player to list of players
    static public void AddPlayer(PlayerController vPC) {
        if (sGM.mPlayer.Contains(vPC)) {
            DebugMsg("AddPlayer Error" + vPC.ToString() + " already in player list");
            return;         //If already in the list don't add player again
        }
        sGM.mPlayer.Add(vPC);   //Add player
    }

    //Remove player to list of players
    static public void RemovePlayer(PlayerController vPC) {
        if (sGM.mPlayer.Contains(vPC)) {
            sGM.mPlayer.Remove(vPC);   //Remove player
            return;
        }
        DebugMsg("RemovePlayer Error" + vPC.ToString() + " not in player list");
    }

    //Get player by index
    static public PlayerController GetPlayer(int vIndex) {
        if (vIndex < sGM.mPlayer.Count) {
            return sGM.mPlayer[vIndex];
        }
        DebugMsg("GetPlayer Error index " + vIndex + "Out of range");
        return null;
    }

    static  public  List<PlayerController> PlayerList {
        get {
            return sGM.mPlayer;
        }
    }

    static public PlayerController LocalPlayer {
        get {
            if(sGM!=null && sGM.mPlayer!=null) {
                return  sGM.mPlayer.Find(tPC => tPC.isLocalPlayer);
            }
            return null;
        }
    }

    static public int PlayerCount {
        get {
            return sGM.mPlayer.Count;
        }
    }
    #endregion

    #region Landscape

    Terrain mTerrain;

    public  static Terrain Terrain {
        get {
            if (sGM != null) {
                if(sGM.mTerrain!=null) {
                    return sGM.mTerrain;        //Return Cached version
                }
                sGM.mTerrain = FindObjectOfType<Terrain>();     //If not cached find it
                return sGM.mTerrain;
            }
            return null;
        }
    }
    #endregion


    #region Helpers

    #endregion

}
