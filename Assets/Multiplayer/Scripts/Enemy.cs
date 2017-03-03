using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

	[SyncVar(hook = "OnChangeColor")]
	public	Color	mColour;

	// Use this for initialization



	// Update is called once per frame
	void Update () {
	
	}

	[ClientRpc]
	public	void	RpcNewName(string vName) {
		name=vName;
	}

	public	void	NewColour(Color vColour) {
		if (isServer) {
			mColour = vColour;
		}
	}

	void	Start() {
		Debug.Log("Enemy Start():" + ((isServer)?"Server":"Client") + name);
	}

	void	OnChangeColor(Color vColour) {
		GetComponent<MeshRenderer> ().material.color = vColour;
	}
}
