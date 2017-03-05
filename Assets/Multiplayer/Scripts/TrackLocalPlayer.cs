using UnityEngine;
using System.Collections;

namespace Multiplayer {

	public class TrackLocalPlayer : MonoBehaviour {

		PlayerController	mLocalPlayer;		//Private cached copy
		
		public	PlayerController	LocalPlayer {		//Set Local player to track
			set {
				mLocalPlayer = value;
			}
		}

		Vector3	mIntialPosition;		//Keep start positions, as defautl & relative offset
		Quaternion	mInitalRotation;

		// Use this for initialization
		void Start () {
			mIntialPosition = transform.position;		//Get initial camera offset
			mInitalRotation = transform.rotation;			//Get inital rotation
		}
		
		// Update is called once per frame
		void LateUpdate () {
			if (mLocalPlayer != null) {	//Only if we are tracking a local player
				transform.position=mLocalPlayer.CameraOffset.position;		//Move to relative player offset
				transform.LookAt(mLocalPlayer.transform.position);		//Look at player
			} else {
				transform.position = mIntialPosition;		//Default position
				transform.rotation = mInitalRotation;
			}
		}
	}
}