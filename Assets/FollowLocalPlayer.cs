using UnityEngine;
using System.Collections;

public class FollowLocalPlayer : MonoBehaviour {

    Vector3 mStartingPosition;
    Quaternion mStartingRotation;

	// Use this for initialization
	void Start () {
        mStartingPosition = transform.position;
        mStartingRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        PlayerController tPC = GM.LocalPlayer;
        if(tPC!=null) {
            transform.position = tPC.transform.position+ tPC.transform.rotation*mStartingPosition;    //Relative to player, keeping behind
            transform.LookAt(tPC.transform.position, Vector3.up);
        } else {
            transform.position = mStartingPosition;    //Default look
            transform.rotation = mStartingRotation;
        }
	}
}
