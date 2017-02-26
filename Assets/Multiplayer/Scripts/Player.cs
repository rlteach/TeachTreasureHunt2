using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using RL_Helpers;		//Helper code

namespace Multiplayer {
    public class Player : Entity {

		public override EType Type {
			get {
				return	(isLocalPlayer)?EType.LocalPlayer:EType.RemotePlayer;		//Is it a local player or the remote one
			}
		}

		void	Start() {
			name = ((isLocalPlayer)?"Local Player":"Remote Player");
		}

	    public override void OnStartLocalPlayer() {	//Called for local player start, can be used to differenticate local player
			base.OnStartLocalPlayer();		//Make bass class process itself
		    gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;	//Make local player blue
	    }

	    // Process local player, NPC objects processed on server and other players on their clients
		public	override	void	ProcessLocalPlayer () {
			DoMovePlayer ();
			DoFire ();	//Process fire request
		}

		#region Move
	    void	DoMovePlayer() {		//Move local player, Network Transform component will send this to server
			float	tRotate = IC.GetInput(IC.Directions.MoveX) *360f*Time.deltaTime;
			float	tMove = IC.GetInput(IC.Directions.MoveY)*10f*Time.deltaTime;
		    transform.Rotate(0,tRotate,0);
		    transform.position+=transform.TransformDirection (Vector3.forward*tMove);
	    }
		#endregion

		#region Fire		//First request is handled on client, but its acted on by server using a command
		public	GameObject	BulletPrefab;
		public	Transform	BulletSpawn;

		void	DoFire() {	//Process fire command locally
			if (Input.GetKeyDown (KeyCode.Space)) {
				CmdDoFire ();	//However to bullets are like NPC so tell the server to fire
			}
		}

		//Run this code remotely on the server
	    [Command]	//Marks this as a server command
	    void	CmdDoFire() {	//Note Cmd prefix, needed for remote command
		    var tBullet=Instantiate (BulletPrefab,BulletSpawn.position,BulletSpawn.rotation) as GameObject;	//Make bullet from prefab on server
		    var	tRB = tBullet.GetComponent<Rigidbody> ();	//Simple physics fire
		    tRB.velocity = tBullet.transform.forward * 3f;	//Bullet moves forward
		    Destroy (tBullet, 2f);	//Bullets last 2 seconds
		    tBullet.name = name+"-Bullet";	//Label them
		    NetworkServer.Spawn(tBullet);	//Tell server to spawn this on all the connected clients
	    }
		#endregion

    }
}

