using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace Multiplayer {
    public class Bullet : Entity {

        [HideInInspector]
        public PlayerController PC;

        public  float Speed=20f;

		public override EType Type {
			get {
				return	EType.Bullet;
			}
		}

		bool	isTarget(Entity vTarget) {		//Return true for valid bullet target
			return	(vTarget.Type == EType.RemotePlayer || vTarget.Type == EType.LocalPlayer);
		}

		public	override	void	OnStartServer() {
            Rigidbody tRB = GetComponent<Rigidbody>();
            Collider tCOL = GetComponent<Collider>();
            MeshRenderer tMesh = GetComponent<MeshRenderer>();
            tRB.velocity = PC.transform.forward * 10f;
            transform.position = PC.BulletSpawn.position;
            transform.rotation = PC.BulletSpawn.rotation;
            tMesh.enabled = true;
            tCOL.enabled = true;
            Destroy(gameObject, 2f);    //Bullets last limited time
        }

        public override	void	OnStartClient() {		//Ignore base startup code
		}


		protected override	void	CollidedWith(Entity vOther, bool vIsTrigger) {
			if (isTarget (vOther)) {			//Bullets should only hit player
				PlayerController	tPlayer = (PlayerController)vOther;		//We know its a player, so cast it
				Destroy (gameObject);	//Kill bullet
				tPlayer.TakeHit (1);		//Knock down player health
			}
		}
    }
}
