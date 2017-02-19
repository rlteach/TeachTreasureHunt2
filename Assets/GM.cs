using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : Singleton {

    public static   bool DebugOn = true;

	private	static	GM	sGM;        //Used to keep singleton reference

    private List<PlayerController> mPlayer = new List<PlayerController>();      //Keep list of Players in game

	void Awake () {
		if (CreateSingleton (ref sGM)) {
		}
	}

    public  static  void    Msg(string vDebugMsg) {
        if(DebugOn) {
            Debug.Log(vDebugMsg);
        }
    }



    static  public  void AddPlayer(PlayerController vPC) {
        if(sGM.mPlayer.Contains(vPC)) {
            DebugMsg("AddPlayer Error" + vPC.ToString() + " not in player list");
            return;         //If already in the list don't add player again
        }
        sGM.mPlayer.Add(vPC);   //Add player
    }

    static public void RemovePlayer(PlayerController vPC) {
        if (sGM.mPlayer.Contains(vPC)) {
            sGM.mPlayer.Remove(vPC);   //Remove player
            return;
        }
        DebugMsg("RemovePlayer Error" + vPC.ToString() + " not in player list");
    }

    static public PlayerController GetPlayer(int vIndex) {
        if (vIndex<sGM.mPlayer.Count) {
            return sGM.mPlayer[vIndex];
        }
        DebugMsg("GetPlayer Error index " + vIndex + "Out of range");
        return null;
    }

    string mDebugText="hello";

    void    ClearDebugText() {
        mDebugText = "";
    }
 
    static public string DebugText {
        get {
            return sGM.mDebugText;
        }

        set {
            sGM.mDebugText = value;
            sGM.Invoke("ClearDebugText",3f);
        }
    }
}
