using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPlayerStats : MonoBehaviour {

    Text    mText;
	// Use this for initialization
	void Start () {
        mText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        mText.text = string.Format("Water {0}", GM.GetPlayer(0).Water);
	}
}
