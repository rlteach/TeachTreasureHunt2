using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class ObjectSpawner : NetworkBehaviour {

    List<Entity> mObjectList = new List<Entity>();

    public GameObject[] ObjectPrefab;
    public int numberOfEnemies = 10;

    public override void OnStartServer() {
        for (int i = 0; i < numberOfEnemies; i++) {
            var tSpawnPosition = new Vector3(Random.Range(-8.0f, 8.0f),0.0f,Random.Range(-8.0f, 8.0f));

            var tSpawnRotation = Quaternion.Euler(0.0f,Random.Range(0, 180),0.0f);
            tSpawnPosition.y = GM.Terrain.SampleHeight(tSpawnPosition); //Adjust for height
            var tEnemy = (GameObject)Instantiate(ObjectPrefab[Random.Range(0, ObjectPrefab.Length)], tSpawnPosition + transform.position, tSpawnRotation);
            mObjectList.Add(tEnemy.GetComponent<Entity>());
            NetworkServer.Spawn(tEnemy);
        }
    }
}
