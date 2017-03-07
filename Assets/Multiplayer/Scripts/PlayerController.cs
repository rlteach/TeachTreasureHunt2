using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using RL_Helpers;		//Helper code

namespace Multiplayer {

    public class PlayerController : Entity {

        Cooldown mCoolDown=new Cooldown(0.5f);

        #region Startup
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
            mInventoryItems.Callback = OnInventoryChanged;
        }

        public override	void	OnStartLocalPlayer() {
			base.OnStartLocalPlayer();		//Print Debug
			name = "Local Player";		//Change name
			mCC=GetComponent<CharacterController>();
			gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;	//Make local player blue
			CmdResetHealth();
			StartCameraTrack ();
            GM.LocalPlayer = this;
		}

        #endregion

        #region Tracking
        public Transform	CameraOffset;       //If local player set camera to track player

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
        private CharacterController mCC;

        Vector3 mMoveDirection =Vector3.zero;
		public	float	MoveSpeed = 3f;
		public	float	JumpHeight = 10f;

	    void	DoMovePlayer() {		//Move local player, Network Transform component will send this to server
			if (mCC.isGrounded&& mHealth>0) {
				transform.Rotate (0, IC.GetInput (IC.Directions.MoveX), 0);
				mMoveDirection.x = 0f;
				mMoveDirection.y = 0f;
				mMoveDirection.z = IC.GetInput (IC.Directions.MoveY);
				mMoveDirection = transform.TransformDirection (mMoveDirection);      //Move in direction character is facing
				mMoveDirection *= MoveSpeed;
				if (IC.GetInput (IC.Directions.Jump) > 0f) {
					mMoveDirection.y = JumpHeight;        //Jump
				}
			}
			mMoveDirection.y += Physics.gravity.y * Time.deltaTime;
			mCC.Move (mMoveDirection * Time.deltaTime);
	    }
		#endregion

		#region Fire		//Request is handled on client, but its acted on by server using a command
		public	GameObject	BulletPrefab;
		public	Transform	BulletSpawn;

		void	DoFire() {	//Process fire command locally
			if (mCoolDown.Cool(Time.deltaTime) &&  IC.GetInput(IC.Directions.Fire)>0f) {
				CmdDoFire ();	//Bullets are like NPC's so tell the server to fire
			}
		}

		//Run this code remotely on the server
	    [Command]	//Marks this as a server command
	    void	CmdDoFire() {	//Note Cmd prefix, needed for remote command
			var tBulletGO=Instantiate (BulletPrefab) as GameObject;	//Make bullet from prefab on server
			var tBullet=tBulletGO.GetComponent<Bullet>();
            tBullet.PC = this;                  //Link back to owner
		    tBulletGO.name = name+"-Bullet";	//Label them
            NetworkServer.Spawn(tBulletGO); //Tell server to spawn this on all the connected clients
        }
        #endregion


        #region  Health


        private HealthBar mHealthBar;

        [SyncVar(hook = "OnChangeHealth")]      //Sync Var on Server & local change hook
        public int mHealth;

        [Command]
		public	void	CmdResetHealth() {
			mHealth = 100;
		}

		[Command]
		public	void	CmdResetHealthAfter() {
			StartCoroutine (ResetHealthAfter(5f));
		}

		IEnumerator	ResetHealthAfter(float vTime) {
			yield	return		new WaitForSeconds(vTime);
			mHealth = 100;
		}

		public void TakeHit(int vAmount)
		{
			if (isServer) {
				mHealth -= vAmount;
				if (mHealth <= 0) {
					mHealth = 0;
					CmdResetHealthAfter ();	//Delay Health reset
                }
				mHealthBar.Health = mHealth;
            }
        }

		void	OnChangeHealth(int vNewHealth) {
			mHealth = vNewHealth;		//This was missing and caused the bug, as the UpdateCallback changed the healthbar, but not the underlying local variable
			mHealthBar.Health = vNewHealth;
			DB.Message(string.Format("{0} {1} Client:{2} Server:{3}",name,System.Reflection.MethodBase.GetCurrentMethod().Name,isClient,isServer));	//Print where we are		
		}
        #endregion

        protected override void CollidedWith(Entity vOther, bool vIsTrigger) {      //Player Collides with Pickups
            if(vOther.Type==EType.Pickup) {
                int tType = vOther.GetComponent<Pickup>().PickupType;
                Destroy(vOther.gameObject);
                mInventoryItems.Add(tType);
            }
        }


        #region Inventory
        public SyncListInt mInventoryItems = new SyncListInt();        //Make Syncronised list of items picked up;

        private void OnInventoryChanged(SyncListInt.Operation op, int index) {
            Debug.Log("Items changed " + op);
            if(GM.InventoryPanel!=null) {
                GM.InventoryPanel.ShowUpdated();
            }
        }

        public  int CountInventoryItemsOfType(int vItem) {
            int tItems = 0;

            foreach(int tItem in mInventoryItems) {
                if(tItem==vItem) {
                    tItems++;
                }
            }
            return  tItems;
        }

    }

    #endregion

}

