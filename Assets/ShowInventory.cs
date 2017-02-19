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
		int	tIndex = 0;
        foreach (Transform child in transform) {
			Text	tText = child.gameObject.GetComponentInChildren<Text> ();	//Get Text for button
			if (tIndex < tPC.Inventory.Count) {
				tText.text = tPC.Inventory [tIndex].Name;
				child.gameObject.SetActive (true);
			} else {
				tText.text = "Blank";
			}
        }
            if (tPC!=null) {
            foreach(var tPickup in tPC.Inventory) {

            }
        }
    }
}
