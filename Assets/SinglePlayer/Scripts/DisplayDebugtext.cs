using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace SinglePlayer {
    public class DisplayDebugtext : MonoBehaviour {

        Text mDebugText;

        // Use this for initialization
        void Start() {
            mDebugText = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update() {
            mDebugText.text = GM.DebugText;
        }
    }
}
