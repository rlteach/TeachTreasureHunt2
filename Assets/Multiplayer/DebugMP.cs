using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugMP : MonoBehaviour {

	Text	mDebugtext;

	// Use this for initialization
	void Start () {
		mDebugtext = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		MovePlayer[] mGOs = FindObjectsOfType<MovePlayer> ();
		mDebugtext.text = "";
		foreach (var tP in mGOs) {
			string	tType = (tP.isLocalPlayer) ? "Local" : "Remote";
			mDebugtext.text += tP.name + tType + "\n";
		}
	}

}
