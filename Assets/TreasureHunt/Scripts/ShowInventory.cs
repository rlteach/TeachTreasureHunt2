using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShowInventory : MonoBehaviour {

    public  GameObject InventoryPrefab;

    readonly int ItemCount = 10;

    GameObject[]    mButtonGO;

    float tUpdateTicker = 0;
        
    void Start() {
        mButtonGO = new GameObject[ItemCount];
        for (int tI=0;tI< ItemCount; tI++) {
            mButtonGO[tI] = Instantiate(InventoryPrefab);       //Create Buttons to use later
            mButtonGO[tI].transform.SetParent(transform);     //Parent to Panel
            mButtonGO[tI].SetActive(false);       //Don't show yet
        }
    }

    // Update is called once per frame
    void Update () {
        if (tUpdateTicker > 0) {
            tUpdateTicker -= Time.deltaTime;
        } else {
            Show();     //Only redisplay Inventory every half second, as its expensive
            tUpdateTicker = 0.5f;
        }
    }

    void    Show() {
        PlayerController tPC = GM.GetPlayer(0);         //This is for player 1
        for (int tIndex = 0; tIndex < transform.childCount; tIndex++) {     //Step through the inventory slots
            ProcessButton tPB = mButtonGO[tIndex].GetComponent<ProcessButton>();        //Get the button
            Text tText = mButtonGO[tIndex].GetComponentInChildren<Text>();      //Get the text to label it from Item
            if (tIndex < tPC.Inventory.Count) {         //Do we have an item in that slot?
                tPB.PC = tPC;       //Link Button to Player
                tPB.OnClick = tPC.Inventory[tIndex].UseItem;            //Link the button code to get processed by Item
                tText.text = tPC.Inventory[tIndex].ToString();      //Label button
                mButtonGO[tIndex].SetActive(true);          //Show it
            } else {
                mButtonGO[tIndex].SetActive(false);     //Don't show if no item in that slot
                tText.text = "Blank:" + tIndex;       //Debug show placeholder
            }
        }
    }
}
