using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace SinglePlayer {
    public class ShowPlayerStats : MonoBehaviour {

        public int Player = 0;

        Text mText;
        // Use this for initialization
        void Start() {
            mText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update() {
            mText.text = string.Format("Water {0}\nFood {1}", GM.GetPlayer(Player).Water, GM.GetPlayer(Player).Food);
        }
    }
}