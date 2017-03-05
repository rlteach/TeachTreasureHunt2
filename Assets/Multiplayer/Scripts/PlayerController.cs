using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using RL_Helpers;		//Helper code

namespace Multiplayer {
    public class PlayerController : Entity {


		private	CharacterController	mCC;
		private	HealthBar	mHealthBar;

		[SyncVar(hook = "OnChangeHealth")]		//Sync Var on Server & local change hook
		public	int	mHealth;

		public override EType Type {
			get {
				return	(isLocalPlayer)?EType.LocalPlayer:EType.RemotePlayer;		//Is it a local player or the remote one
			}
		}

		public override	void	OnStartServer() {		//Called when Server Entity is started
			base.OnStartServer();		//Print Debug
		}

		public override	void	OnStartClient() {		//Called when Local Entity is started
			base.OnStartClient();		//Print Debug
			name = "Remote Player";
			mHealthBar = GetComponentInChildren<HealthBar> ();
			mHealthBar.Health = mHealth;		//Reflect Healthbar on host client

		}

		public override	void	OnStartLocalPlayer() {
			base.OnStartLocalPlayer();		//Print Debug
			name = "Local Player";		//Change name
			mCC=GetComponent<CharacterController>();
			gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;	//Make local player blue
			CmdResetHealth();
			StartCameraTrack ();
		}

		#region Tracking
		public	Transform	CameraOffset;

		void	StartCameraTrack() {
			TrackLocalPlayer	tTrack = Camera.main.gameObject.GetComponent<TrackLocalPlayer> ();
			if (tTrack != null) {
				tTrack.LocalPlayer = this;
			}
		}
		#endregion

	    // Process local player, NPC objects processed on server and other players on their clients
		public	override	void	ProcessLocalPlayer () {
			DoMovePlayer ();
			DoFire ();	//Process fire request
		}

		#region Move
		Vector3	mMoveDirection=Vector3.zero;
		public	float	MoveSpeed = 10f;
		public	float	JumpHeight = 10f;

	    void	DoMovePlayer() {		//Move local player, Network Transform component will send this to server
			if (mCC.isGrounded) {
				transform.Rotate(0, IC.GetInput(IC.Directions.MoveX), 0);
				mMoveDirection.x = 0f;
				mMoveDirection.y = 0f;
				mMoveDirection.z = IC.GetInput(IC.Directions.MoveY);
				mMoveDirection = transform.TransformDirection(mMoveDirection);      //Move in direction character is facing
				mMoveDirection *= MoveSpeed;
				if (IC.GetInput(IC.Directions.Jump) > 0f) {
					mMoveDirection.y = JumpHeight;        //Jump
				}
			}
			mMoveDirection.y += Physics.gravity.y * Time.deltaTime;
			mCC.Move(mMoveDirection * Time.deltaTime);
	    }
		#endregion

		#region Fire		//Request is handled on client, but its acted on by server using a command
		public	GameObject	BulletPrefab;
		public	Transform	BulletSpawn;

		void	DoFire() {	//Process fire command locally
			if (IC.GetInput(IC.Directions.Fire)>0f) {
				CmdDoFire ();	//Bullets are like NPC's so tell the server to fire
			}
		}

		//Run this code remotely on the server
	    [Command]	//Marks this as a server command
	    void	CmdDoFire() {	//Note Cmd prefix, needed for remote command
			var tBulletGO=Instantiate (BulletPrefab) as GameObject;	//Make bullet from prefab on server
			var tBullet=tBulletGO.GetComponent<Bullet>();
		    tBulletGO.name = name+"-Bullet";	//Label them
			tBullet.PC=this;		//Link Server bullet to server player
		    NetworkServer.Spawn(tBulletGO);	//Tell server to spawn this on all the connected clients
	    }
		#endregion


		#region  Health


		[Command]
		public	void	CmdResetHealth() {
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
			DB.Message(string.Format("{0} {1} Client:{2} Server:{3}",name,System.Reflection.MethodBase.GetCurrentMethod().Name,isClient,isServer));	//Print where we are		
			mHealthBar.Health = vNewHealth;
		}
		#endregion

    }
}

