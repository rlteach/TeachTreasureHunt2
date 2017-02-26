using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ObjectSpawner : NetworkBehaviour {

    List<Entity> mObjectList = new List<Entity>();

    public GameObject[] ObjectPrefab;
    public int numberOfPickups = 10;

	Coroutine	mSpawner;

    public override void OnStartServer() {
		GM.OS = this;
		mObjectList.Clear();
		StartSpawn (true);
		GM.DebugMsg ("Started Spawner");
    }


	void	StartSpawn(bool vStart) {
		if (mSpawner != null) {
			StopCoroutine (mSpawner);
			mSpawner = null;
		}
		if (vStart) {
			mSpawner = StartCoroutine (SpawnPickups ());
		}
	}
		
	IEnumerator SpawnPickups () {
		while (mObjectList.Count < numberOfPickups && ObjectPrefab.Length > 0) {		//RespaSpwn upto limit
			var tSpawnPosition = new Vector3 (Random.Range (-8.0f, 8.0f), 0.0f, Random.Range (-8.0f, 8.0f));
			var tSpawnRotation = Quaternion.Euler (0.0f, Random.Range (0, 180), 0.0f);
			tSpawnPosition.y = GM.Terrain.SampleHeight (tSpawnPosition); //Adjust for height
			var tPickup = (GameObject)Instantiate (ObjectPrefab [Random.Range (0, ObjectPrefab.Length)], tSpawnPosition + transform.position, tSpawnRotation);
			AddPickup (tPickup);
			NetworkServer.Spawn (tPickup);
			yield return new WaitForSeconds (2);
		}
	}

	void	AddPickup(GameObject tGO) {
		Entity	tE = tGO.GetComponent<Entity> ();
		if (tE != null) {
			mObjectList.Add (tE);
			GM.DebugMsg (System.Reflection.MethodBase.GetCurrentMethod ().Name + "Added:" + tGO.name);
		} else {
			GM.DebugMsg (System.Reflection.MethodBase.GetCurrentMethod ().Name + "No Entity component");
		}
	}

	[Command]
	public	void	CmdRemovePickup(GameObject tGO) {
		Entity	tE = tGO.GetComponent<Entity> ();
		if (tE != null) {
			if (mObjectList.Contains (tE)) {
				mObjectList.Remove (tE);
				GM.DebugMsg (System.Reflection.MethodBase.GetCurrentMethod ().Name + "Removed:" + tGO.name);
			} else {
				GM.DebugMsg (System.Reflection.MethodBase.GetCurrentMethod ().Name + "Not in list");
			}
		} else {
			GM.DebugMsg (System.Reflection.MethodBase.GetCurrentMethod ().Name + "No Entity component");
		}
	}
}
