using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugPanel : MonoBehaviour {

    [Header("Player Debug")]
    public  GameObject PlayerDebugDisplayPrefab;

    List<PlayerDebugDisplay> mPlayerDebugList = new List<PlayerDebugDisplay>();

    void Start() {
        GM.DebugMsg(PlayerDebugDisplayPrefab.name);
        InvokeRepeating("UpdatePlayers", 1f, 1f);
    }


    void   UpdatePlayers() {
        for( int tIndex =0; tIndex<Mathf.Max(GM.PlayerCount,mPlayerDebugList.Count); tIndex++) {
            if (tIndex < GM.PlayerCount) {       //For all players
                if (tIndex < mPlayerDebugList.Count) {    //Do we already have a debug item
                    mPlayerDebugList[tIndex].PC = GM.GetPlayer(tIndex);     //Link Player to it
                } else {
                    var tGO = Instantiate(PlayerDebugDisplayPrefab) as GameObject;       //If we dont have one make one
                    var tPC = tGO.GetComponent<PlayerDebugDisplay>();
                    tPC.PC = GM.GetPlayer(tIndex);     //Link Player to it
                    tGO.transform.SetParent(transform);     //Parent to Panel
                    mPlayerDebugList.Add(tPC);
                }
            } else {
                var tPCD = mPlayerDebugList[tIndex];
                mPlayerDebugList.Remove(tPCD);      //Remove it if no corresponding player
                Destroy(tPCD.gameObject);
            }
        }
    }
}
