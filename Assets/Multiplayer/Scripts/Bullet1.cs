using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet1 : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider vOther) {
		Health1 tH = vOther.GetComponent<Health1> ();
		if (tH != null) {
			Destroy (gameObject);
			tH.TakeDamage (15);
//			CmdHit (tNID.netId);
		}
		Enemy	tE = vOther.GetComponent<Enemy> ();
		if (tE != null) {
			tE.NewColour(Random.ColorHSV());
		}
	}
/*
	[Command]
	void	CmdHit(NetworkInstanceId vNID) {
		GameObject obj;
		if (Game.networkManager.isClient)
			obj = ClientScene.FindLocalObject (vNID);
		else
			obj = NetworkServer.FindLocalObject (vNID);  
		//do something with obj		Debug.Log ("Hit:" + vMP.name);
	}
*/
}
