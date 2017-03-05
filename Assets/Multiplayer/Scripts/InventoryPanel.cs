using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Multiplayer {
    public class InventoryPanel : MonoBehaviour {

        public Sprite[] Images;         //Link Images in here

        public GameObject InventoryPrefab;      //Prefab for defautl item

        GameObject[] mInventoryItems;       //Cache of Gameobject from prefab to use as Items

        void Start() {
            GM.InventoryPanel = this;
            mInventoryItems = new GameObject[Images.Length];        //Make Space for them all
            for (int tI=0; tI< mInventoryItems.Length;tI++) {
                mInventoryItems[tI] = Instantiate(InventoryPrefab);
                mInventoryItems[tI].SetActive(false);       //Don't show initially
                ShowUpdated();
            }
        }

        public  void    ShowUpdated() {     //Called when list is updates
            if(GM.LocalPlayer!=null) {
                for(int tI=0;tI< mInventoryItems.Length;tI++) {         //Look for items of each known type
                    int tCount= GM.LocalPlayer.CountInventoryItemsOfType(tI);   //Count them
                    Image tImage = mInventoryItems[tI].GetComponent<Image>();       //Get Image and text to give it cottect rendition
                    Text tText = mInventoryItems[tI].GetComponentInChildren<Text>();
                    if (tCount>0) {     //If we have item(s) then display them
                        mInventoryItems[tI].transform.SetParent(gameObject.transform); //Parent to panel transform
                        tImage.sprite = Images[tI];
                        tText.text = tCount.ToString();
                        mInventoryItems[tI].SetActive(true);
                    } else {        //If not dotn show anythign in that slot
                        mInventoryItems[tI].SetActive(false);
                        mInventoryItems[tI].transform.SetParent(null); //UnParent from panel transform
                        tImage.sprite = null;
                        tText.text = "???"; //Debug, should never show
                    }
                }
            }            
        }
    }
}
