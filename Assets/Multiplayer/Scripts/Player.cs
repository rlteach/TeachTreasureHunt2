using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using RL_Helpers;		//Helper code

namespace Multiplayer {
    public class Player : Entity {



		private	HealthBar	mHealthBar;

		[SyncVar(hook = "OnChangeHealth")]		//Sync Var on Server & local change hook
		public	int	mHealth;

		public override EType Type {
			get {
				return	(isLocalPlayer)?EType.LocalPlayer:EType.RemotePlayer;		//Is it a local player or the remote one
			}
		}

		protected override	void	OnStartServerEntity() {		//Called when Local Entity is started
			base.OnStartServerEntity();		//Print Debug
		}

		protected override	void	OnStartClientEntity() {		//Called when Local Entity is started
			base.OnStartClientEntity();		//Print Debug
			name = "Local Player";
			mHealthBar = GetComponentInChildren<HealthBar> ();
		}

		protected override	void	OnIsPlayerEntity() {
			base.OnIsPlayerEntity();		//Print Debug
			name = "Remote Player";		//Change name
			gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;	//Make local player blue
		}

	    // Process local player, NPC objects processed on server and other players on their clients
		public	override	void	ProcessLocalPlayer () {
			DoMovePlayer ();
			DoFire ();	//Process fire request
		}

		#region Move
	    void	DoMovePlayer() {		//Move local player, Network Transform component will send this to server
			float	tRotate = IC.GetInput(IC.Directions.MoveX) *180*Time.deltaTime;
			float	tMove = IC.GetInput(IC.Directions.MoveY)*10f*Time.deltaTime;
		    transform.Rotate(0,tRotate,0);
		    transform.position+=transform.TransformDirection (Vector3.forward*tMove);
	    }
		#endregion

		#region Fire		//First request is handled on client, but its acted on by server using a command
		public	GameObject	BulletPrefab;
		public	Transform	BulletSpawn;

		void	DoFire() {	//Process fire command locally
			if (IC.GetInput(IC.Directions.Fire)>0f) {
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


		#region  Health

		[Command]
		public	void	CmdReset() {
			mHealth = 100;
		}

		public void TakeHit(int vAmount)
		{
			if (isServer) {
				mHealth -= vAmount;
				if (mHealth <= 0) {
					mHealth = 0;
				}
				mHealthBar.Health = mHealth;
			}
		}

		void	OnChangeHealth(int vNewHealth) {
			mHealthBar.Health = vNewHealth;
		}
		#endregion

    }
}

