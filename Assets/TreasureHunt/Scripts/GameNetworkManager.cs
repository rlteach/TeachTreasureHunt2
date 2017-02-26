using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetworkManager : NetworkManager {

	public override void OnStartServer() {
		base.OnStartServer ();
		GM.DebugMsg ("OnStartServer()");
	}
	public override void OnStopServer() {
		base.OnStopServer ();
		GM.DebugMsg ("OnStopServer()");
	}
}
