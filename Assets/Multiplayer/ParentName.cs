using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParentName : MonoBehaviour {

	Text	mText;
	// Use this for initialization
	void Start () {
		mText = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent != null) {
			mText.text = transform.parent.name;
		} else {
			mText.text = "No parent";
		}
	}
}
