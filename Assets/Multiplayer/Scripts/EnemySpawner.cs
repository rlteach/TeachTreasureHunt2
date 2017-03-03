using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
namespace Multiplayer {

	public class EnemySpawner : NetworkBehaviour {

		List<Enemy>	mEnemyList=new List<Enemy>();

		public GameObject EnemyPrefab;
		public int numberOfEnemies=10;

		public override void OnStartServer()
		{
			for (int i=0; i < numberOfEnemies; i++)
			{
				var tSpawnPosition = new Vector3(
					Random.Range(-8.0f, 8.0f),
					0.0f,
					Random.Range(-8.0f, 8.0f));

				var tSpawnRotation = Quaternion.Euler( 
					0.0f, 
					Random.Range(0,180), 
					0.0f);

				var tEnemy = (GameObject)Instantiate(EnemyPrefab, tSpawnPosition+transform.position, tSpawnRotation);
				mEnemyList.Add(tEnemy.GetComponent<Enemy>());
				NetworkServer.Spawn(tEnemy);
			}
			Invoke ("UpdateEnemy", 5f);
		}

		void		UpdateEnemy() {
			int	tI = 1;
			foreach (var tE in mEnemyList) {
				tE.RpcNewName(string.Format("Enemy {0}",tI++));
			}
		}
	}
}
