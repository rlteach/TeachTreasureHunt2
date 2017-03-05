using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Multiplayer {
    public class Spawner : NetworkBehaviour {

        public  GameObject PickupPrefab;

        List<Pickup> mPickupList = new List<Pickup>();

        public override void OnStartServer() {
            InvokeRepeating("DoSpawn",2f,3f);
        }

        public  void OnStopServer() {
            CancelInvoke();
        }

        void DoSpawn() {
            if (isServer) { 
                var tPickupGO = Instantiate(PickupPrefab) as GameObject;    //Make Pickup from prefab on server
                var tPickup = tPickupGO.GetComponent<Pickup>();
                Vector3 tPosition = Quaternion.Euler(0, Random.Range(-45f, 45f), 0) * Vector3.forward * 10f;
                PlayerController tRandomPlayer = RandomPlayer();
                if(tRandomPlayer!=null) {
                    tPosition += tRandomPlayer.transform.position;
                    Debug.Log("Player Spawn:" + tRandomPlayer.netId);
                }
                tPickup.transform.position = tPosition;
                Destroy(tPickupGO, 10f);
                NetworkServer.Spawn(tPickupGO); //Tell server to spawn this on all the connected clients
            }
        }

        PlayerController RandomPlayer() {
            PlayerController[] tRandomArray = FindObjectsOfType<PlayerController>();
            if(tRandomArray.Length>0) {
                return tRandomArray[Random.Range(0, tRandomArray.Length)];
            }
            return null;
        }
    }
}
