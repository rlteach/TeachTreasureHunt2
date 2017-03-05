using UnityEngine;
using System.Collections;

using UnityEngine.UI;

namespace Multiplayer {
    public class InventoryItem : MonoBehaviour {

        public Sprite[] Images;         //Link Images in here

        public GameObject InventoryPrefab;

        public int Item {
            set {
                if (value>=0 && value < Images.Length) {
                    mImage.sprite = Images[value];
                } else {
                    mImage.sprite = null;
                }
                mItem = value;
                Count = mCount;
            }
            get {
                return mItem;
            }
        }

        public int Count {
            set {
                if (mImage.sprite!=null) {
                    mCountText.text = value.ToString();
                } else {
                    mCountText.text = "ERR";
                }
                mCount = value;
            }
            get {
                return mCount;
            }
        }


        Image   mImage;
        Text    mCountText;

        int mItem;

        int mCount;

        // Use this for initialization
        void Start() {
            mImage = GetComponent<Image>();
            mCountText = GetComponentInChildren<Text>();
            Item = -1;
        }
    }
}
