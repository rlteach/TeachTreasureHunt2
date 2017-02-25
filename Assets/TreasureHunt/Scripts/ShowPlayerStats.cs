using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPlayerStats : MonoBehaviour {


    Text    mText;
    PlayerController mPC;


	// Use this for initialization
	void Start () {
        mText = GetComponentInChildren<Text>();
        mPC = transform.parent.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
       transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        mText.text = (mPC.isLocalPlayer) ? "Local" : "Remote";
	}
}
