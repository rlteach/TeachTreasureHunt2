using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowInventory : MonoBehaviour {

    public  GameObject InventoryPrefab;

    List<GameObject> mInventoryButtons=new List<GameObject>();      //List of buttons

    void Start() {
        for(int tI=0;tI<10;tI++) {
            GameObject tGO = Instantiate(InventoryPrefab);
            mInventoryButtons.Add(tGO);    //Make 10 buttons ready to display
            tGO.transform.SetParent(transform);     //Parent to Panel
//            tGO.SetActive(false);       //Don't show yet
        }
    }

    // Update is called once per frame
    void Update () {
        PlayerController tPC = GM.GetPlayer(0);
        foreach (Transform child in transform) {

        }
            if (tPC!=null) {
            foreach(var tPickup in tPC.Inventory) {

            }
        }
    }
}
